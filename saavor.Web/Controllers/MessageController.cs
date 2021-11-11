using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using saavor.Application.Messages.Commands.Upsert;
using saavor.Application.Messages.Query;
using saavor.Shared.AppSettings;
using saavor.Shared.Constants;
using saavor.Shared.DTO.Message;
using saavor.Shared.Filter;
using saavor.Shared.Interfaces;
using saavor.Shared.ViewModel;
using saavor.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saavor.Web.Controllers
{
    /// <summary>
    /// MessageController
    /// </summary>
    [Authorize]
    public class MessageController : Controller
    {
        /// <summary>
        /// _claimService
        /// </summary>
        private readonly IClaimService _claimService;

        /// <summary>
        /// _protector
        /// </summary>
        private readonly IDataProtector _protector;

        /// <summary>
        /// _dataProtectionAppSettings
        /// </summary>
        private readonly DataProtection _dataProtectionAppSettings;

        /// <summary>
        /// _PageSizeAppSettings
        /// </summary>
        private readonly PageSize _PageSizeAppSettings;               

        /// <summary>
        /// _logger
        /// </summary>
        private readonly ILogger<MessageController> _logger;

        /// <summary>
        /// _messageUpsertCommand
        /// </summary>
        private readonly MessageUpsertCommand _messageUpsertCommand;

        /// <summary>
        /// _getMessageQuery
        /// </summary>
        private readonly GetMessageQuery _getMessageQuery;

        /// <summary>
        /// _fCMNotifications
        /// </summary>
        private readonly FCMNotifications _fCMNotifications;

        /// <summary>
        /// _aPNSNotification
        /// </summary>
        private readonly APNSNotification _aPNSNotification;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="claimServiceInstance"></param>
        /// <param name="dataProtectionAppSettingsInstacne"></param>
        /// <param name="providerInstance"></param>
        /// <param name="pageSizeAppSettingsInstacne"></param>
        /// <param name="appUsersInstance"></param>
        /// <param name="logger"></param>
        public MessageController(IClaimService claimServiceInstance                                           
                                 ,IOptions<DataProtection> dataProtectionAppSettingsInstacne
                                 ,IDataProtectionProvider providerInstance
                                 ,IOptions<PageSize> pageSizeAppSettingsInstacne
                                 ,IOptions<AppUsers> appUsersInstance                                          
                                 ,ILogger<MessageController> logger
                                 ,MessageUpsertCommand messageUpsertCommandInstance
                                 ,GetMessageQuery getMessageQueryInstance
                                 ,FCMNotifications fCMNotificationsInstance
                                 ,APNSNotification aPNSNotificationInstance)
        {
            _claimService = claimServiceInstance;
            _dataProtectionAppSettings = dataProtectionAppSettingsInstacne.Value;
            _protector = providerInstance.CreateProtector(_dataProtectionAppSettings.Key);
            _PageSizeAppSettings = pageSizeAppSettingsInstacne.Value;            
            _logger = logger;
            _messageUpsertCommand = messageUpsertCommandInstance;
            _getMessageQuery = getMessageQueryInstance;
            _fCMNotifications = fCMNotificationsInstance;
            _aPNSNotification = aPNSNotificationInstance;
        }

        /// <summary>
        /// Messages
        /// </summary>
        /// <returns></returns>
        [Route(CommonConstants.RoutMessages)]
        public IActionResult Messages(int pageNumber, string search)
        {
            int totalRecord = 0;             
            pageNumber = pageNumber == 0 ? 1 : pageNumber;
            ViewData["CurrentFilter"] = search;

            var input = new MessageDTO()
            {
                MessageId = 0,
                ProfileId = Convert.ToInt64(_claimService.GetClaim(CommonConstants.ProfileId)),
                Page = pageNumber,
                Size = Convert.ToInt32(_PageSizeAppSettings.Size),
                Search = search
            };

            List<MessageModel> messageList = _getMessageQuery.Get(input).Result;
            if (messageList != null && messageList.Count > 0)
            {
                var res = messageList.Select(x => new
                {
                    x.TotalRecord

                }).FirstOrDefault();

                totalRecord = res.TotalRecord;
            }
            ViewBag.Paging = SetPaging.Set_Paging(pageNumber, Convert.ToInt32(_PageSizeAppSettings.Size), totalRecord, "activeLink", Url.Action("messages", "message"), "disableLink", string.Empty);
           
            return View(LoadMessages(messageList));
        }

        /// <summary>
        /// Message
        /// </summary>
        /// <returns></returns>
        [Route(CommonConstants.RoutMessage)]
        public IActionResult Message()
        {
            return View();
        }

        /// <summary>
        /// UpsertMessage
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        public IActionResult UpsertMessage(string messageId, string subject, string message)
        {
            try
            {
                Int64 messageID = 0;
                if(!string.IsNullOrEmpty(messageId))
                {
                    if (!(Int64.TryParse(_protector.Unprotect(messageId), out messageID)))
                    {
                        messageID = 0;
                    }
                }

                var input = new MessageDTO() {
                    MessageId = messageID,
                    ProfileId = Convert.ToInt64(_claimService.GetClaim(CommonConstants.ProfileId)),
                    Subject = subject,
                    Message = message,
                    NotificationType = string.Empty
                };

                List<MessageResponseVm> response =  _messageUpsertCommand.Upsert(input).Result;
                if(response != null && response.Count > 0)
                {
                    Task.Factory.StartNew(() =>
                    {
                        foreach(var row in response)
                        {
                            if(row.DeviceType.ToLowerInvariant() == "ios")
                            {
                                APNSNotification.ApnsUserNotification(row.DeviceToken, row.Subject + "-" + row.Message);
                            }
                            else
                            {
                                _fCMNotifications.SendUserFCMNotifications(row.DeviceToken, row.Subject + "-" + row.Message, row.DeviceType);
                            }
                        }
                    });
                }
                return Json(1);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return Json(-1);
            }
            
        }

        /// <summary>
        /// GetMessage
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetMessage(string messageId)
        {
            try
            {
                Int64 messageID = 0;
                if (!string.IsNullOrEmpty(messageId))
                {
                    if (!(Int64.TryParse(_protector.Unprotect(messageId), out messageID)))
                    {
                        messageID = 0;
                    }
                }

                var input = new MessageDTO()
                {
                    MessageId = messageID,
                    ProfileId = Convert.ToInt64(_claimService.GetClaim(CommonConstants.ProfileId))
                };

                MessageModel message = _getMessageQuery.GetById(input).Result;
                return Json(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Json(new MessageModel());
            }

        }

        /// <summary>
        /// DeleteMessage
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DeleteMessage(string messageId)
        {
            try
            {
                Int64 messageID = 0;
                if (!string.IsNullOrEmpty(messageId))
                {
                    if (!(Int64.TryParse(_protector.Unprotect(messageId), out messageID)))
                    {
                        messageID = 0;
                    }
                }

                var input = new MessageDTO()
                {
                    MessageId = messageID,
                    ProfileId = Convert.ToInt64(_claimService.GetClaim(CommonConstants.ProfileId))
                };

                Response response = _messageUpsertCommand.Delete(input).Result;
                return Json(response.Code);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Json(-1);
            }

        }

        /// <summary>
        /// LoadMessages
        /// </summary>
        /// <param name="input"></param>
        /// <returns>MessageVm</returns>
        private List<MessageVm> LoadMessages(List<MessageModel> input)
        {
            List<MessageVm> output = new List<MessageVm>();
            if(input != null && input.Count > 0)
            {
                output = input.Select(item => new MessageVm
                {
                    MessageId = _protector.Protect(Convert.ToString(item.MessageId)),
                    Subject = item.Subject,
                    Message = item.Message,
                    CreateDate = item.CreateDate

                }).ToList();
            }
            return output;
        }
    }
}
