using iText.Html2pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using saavor.Application.Kitchen.Query;
using saavor.Shared.AppSettings;
using saavor.Shared.Constants;
using saavor.Shared.DTO.Kitchen;
using saavor.Shared.DTO.ManageUsers;
using saavor.Shared.Filter;
using saavor.Shared.Interfaces;
using saavor.Shared.ViewModel;
using saavor.Web.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

namespace saavor.Web.Controllers
{
    [Authorize]
    public class KitchenDashboardController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IClaimService _iClaimService;
        private readonly GetKitchenDashboardQuery _getKitchenDashboardQuery;
        private readonly IDataProtector _protector;
        private readonly DataProtection _dataProtectionAppSettings;
        private readonly PageSize _PageSizeAppSettings;
        private readonly AppUsers _appUsers;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _clientFactory;
        private readonly HttpClient _client;
        private readonly ILogger<KitchenDashboardController> _logger;
        public KitchenDashboardController(IClaimService iClaimServiceInstance
                                          , GetKitchenDashboardQuery getKitchenDashboardQueryInstance
                                          , IOptions<DataProtection> dataProtectionAppSettingsInstacne
                                          , IDataProtectionProvider providerInstance
                                          , IHttpContextAccessor httpContextAccessor
                                          , IOptions<PageSize> pageSizeAppSettingsInstacne
                                          , IOptions<AppUsers> appUsersInstance
                                          , IWebHostEnvironment envInitiator
                                          , IHttpClientFactory clientFactory
                                          , HttpClient client
                                          , ILogger<KitchenDashboardController> logger)
        {
            _iClaimService = iClaimServiceInstance;
            _getKitchenDashboardQuery = getKitchenDashboardQueryInstance;
            _dataProtectionAppSettings = dataProtectionAppSettingsInstacne.Value;
            _protector = providerInstance.CreateProtector(_dataProtectionAppSettings.Key);
            _httpContextAccessor = httpContextAccessor;
            _PageSizeAppSettings = pageSizeAppSettingsInstacne.Value;
            _appUsers = appUsersInstance.Value;
            _environment = envInitiator;
            _clientFactory = clientFactory;
            _client = client;
            _logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Overview(string id, string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    name = string.Empty;
                }
                if (string.IsNullOrEmpty(id))
                {
                     return RedirectToAction("home", "dashboard");
                }
                Int64 profileId = 0;
                if (!(Int64.TryParse(_protector.Unprotect(id), out profileId)))
                {
                    profileId = 0;
                }
                TempData[CommonConstants.ProfileId] = Convert.ToString(profileId);
                var input = new saavor.Shared.DTO.Kitchen.KitchenInputDTO()
                {
                    UserId = Convert.ToInt64(_iClaimService.GetClaim(CommonConstants.SaavorUserId)),
                    ProfileId = profileId
                    //ProfileId = 1478
                };

                var model = new KitchenDashboardVm()
                {
                    Count = _getKitchenDashboardQuery.KitchenDashboardCount(input).Result
                };
                ViewData["KitchenName"] = name;
                ViewData[CommonConstants.EncryptedProfileId] = id;
                var userClaims = new List<Claim>()
                        {
                             new Claim(CommonConstants.SaavorUserId, _iClaimService.GetClaim(CommonConstants.SaavorUserId)),
                             new Claim(CommonConstants.SaavorUserEmail, _iClaimService.GetClaim(CommonConstants.SaavorUserEmail)),
                             new Claim(CommonConstants.BusinessName,_iClaimService.GetClaim(CommonConstants.BusinessName)),
                             new Claim(CommonConstants.UserName,_iClaimService.GetClaim(CommonConstants.UserName)),
                             new Claim(CommonConstants.KitchenName,Convert.ToString(name)),
                             new Claim(CommonConstants.ProfileId,Convert.ToString(profileId)),
                             new Claim(CommonConstants.EncryptedProfileId,id),
                             new Claim(CommonConstants.LoginType,_iClaimService.GetClaim(CommonConstants.LoginType)),
                             new Claim(CommonConstants.IsAprovedKitchenRequest,_iClaimService.GetClaim(CommonConstants.IsAprovedKitchenRequest)),
                        };

                _iClaimService.AddClaim(new ClaimsIdentity(userClaims, CommonConstants.UserIdentity)
                                        , userClaims);
                return View(model);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction("error", "home");
            }
        }

        #region Charts Start
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetRevenueChart()
        {
            try
            {
                Int64 profileId = 0;
                if (!(Int64.TryParse(Convert.ToString(TempData[CommonConstants.ProfileId]), out profileId)))
                {
                    profileId = 0;
                }
                var input = new saavor.Shared.DTO.Kitchen.KitchenInputDTO()
                {
                    UserId = Convert.ToInt64(_iClaimService.GetClaim(CommonConstants.SaavorUserId)),
                    ProfileId = profileId
                    //ProfileId = 1478
                };
                return Json(_getKitchenDashboardQuery.KitchenRevenueChart(input).Result);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return Json(new KitchenChartVm());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetOrderSummaryChart(string type)
        {
            try
            {
                Int64 profileId = 0;
                if (!(Int64.TryParse(_iClaimService.GetClaim(CommonConstants.ProfileId), out profileId)))
                {
                    profileId = 0;
                }
                var input = new saavor.Shared.DTO.Kitchen.KitchenInputDTO()
                {
                    UserId = Convert.ToInt64(_iClaimService.GetClaim(CommonConstants.SaavorUserId)),
                    ProfileId = profileId,
                    //ProfileId = 1478,
                    Type = type ?? "Monthly"
                };
                var orderSummaryChart = _getKitchenDashboardQuery.KitchenOrderSummaryChart(input).Result;
                return Json(orderSummaryChart);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Json(new KitchenOrderSummaryVm());
            }
        }
        #endregion Charts End

        #region Manage users
        /// <summary>
        /// ManageUsers
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public IActionResult ManageUsers(int pageNumber, string search)
        {
            try
            {
                int totalRecord = 0;
                pageNumber = pageNumber == 0 ? 1 : pageNumber;
                ViewData["CurrentFilter"] = search;
                var input = new ManageUserInputDTO()
                {
                    UserId = Convert.ToInt64(_iClaimService.GetClaim(CommonConstants.SaavorUserId)),
                    Page = pageNumber,
                    Size = Convert.ToInt32(_PageSizeAppSettings.Size),
                    Search = search,
                    ProfileId = Convert.ToInt64(_iClaimService.GetClaim(CommonConstants.ProfileId)),
                };

                List<KitchenUsersVm> userList = new List<KitchenUsersVm>();
                userList = _getKitchenDashboardQuery.KitchenUsers(input).Result;

                if (userList != null && userList.Count > 0)
                {
                    userList = userList.Select(x =>
                    {
                        x.CreateDate = Convert.ToDateTime(x.CreateDate).ToString("dd-MMM-yyyy hh:mm tt").ToString();
                        return x;
                    }).ToList();
                    var res = userList.Select(x => new
                    {
                        x.TotalRecord
                    }).FirstOrDefault();
                    totalRecord = res.TotalRecord;
                }
                ViewBag.Paging = SetPaging.Set_Paging(pageNumber, Convert.ToInt32(_PageSizeAppSettings.Size), totalRecord, "activeLink", Url.Action("manageusers", "kitchendashboard"), "disableLink", string.Empty);
                return View(userList);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction("error", "home");
            }
        }

        /// <summary>
        /// Get User Information
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetManagerUserInfo(string userId)
        {
            var response = _getKitchenDashboardQuery.KitchenUserInformationById(Convert.ToInt64(userId)).Result;
            if (response != null && !string.IsNullOrEmpty(response.ProfileImagePath))
            {
                string filePath = string.Format("{0}{1}", _appUsers.SaavorUser, response.ProfileImagePath);
                if (!LoadReviewerProfile(filePath))
                {
                    response.ProfileImagePath = _appUsers.DefaultUserImage;
                }
                else
                {
                    response.ProfileImagePath = string.Format("{0}{1}", _appUsers.ProfileImage, response.ProfileImagePath);
                }

            }
            else
            {
                response.ProfileImagePath = _appUsers.DefaultUserImage;
            }

            return PartialView("_ManageUserInformation", response);
        }
        #endregion

        #region Manage Orders
        /// <summary>
        /// ManageOrders
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="search"></param>
        /// <returns></returns>        
        [Route(CommonConstants.RoutAllOrders)]
        public IActionResult ManageOrders(int pageNumber, string search)
        {
            try
            {
                int totalRecord = 0;
                pageNumber = pageNumber == 0 ? 1 : pageNumber;
                ViewData["CurrentFilter"] = search;

                var input = new KitchenInputDTO()
                {
                    UserId = Convert.ToInt64(_iClaimService.GetClaim(CommonConstants.SaavorUserId)),
                    Page = pageNumber,
                    Size = Convert.ToInt32(_PageSizeAppSettings.Size),
                    Search = search,
                    ProfileId = Convert.ToInt64(_iClaimService.GetClaim(CommonConstants.ProfileId)),
                    OrderType = "Pending,Arrived,Preparing,Out for delivery,Delivered,Rejected,Cancelled,Ready"
                };

                List<KitchenManageOrderVm> orderList = new List<KitchenManageOrderVm>();
                orderList = _getKitchenDashboardQuery.KitchenManageOrders(input).Result;

                if (orderList != null && orderList.Count > 0)
                {
                    var res = orderList.Select(x => new
                    {
                        x.TotalRecord
                    }).FirstOrDefault();
                    totalRecord = res.TotalRecord;

                    orderList = orderList.Select(item => new KitchenManageOrderVm
                    {
                        OrderId = item.OrderId,
                        OrderDate = item.OrderDate,
                        UserName = item.UserName,
                        UserEmail = item.UserEmail,
                        OrderStatus = item.OrderStatus,
                        FoodOrderId = _protector.Protect(Convert.ToString(item.Id)),

                    }).ToList();
                }
                ViewBag.Paging = SetPaging.Set_Paging(pageNumber, Convert.ToInt32(_PageSizeAppSettings.Size), totalRecord, "activeLink", Url.Action("manageorders", "kitchendashboard"), "disableLink", string.Empty);
                return View(orderList);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction("error", "home");
            }
        }

        /// <summary>
        /// NewOrders
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        [Route(CommonConstants.RoutNewOrders)]
        public IActionResult NewOrders(int pageNumber, string search)
        {
            try
            {
                pageNumber = pageNumber == 0 ? 1 : pageNumber;
                ViewData["CurrentFilter"] = search;
                var input = LoadInput(pageNumber, search, "Pending,Accepted");
                var orderList = _getKitchenDashboardQuery.KitchenManageOrders(input).Result;
                ViewBag.Paging = SetPaging.Set_Paging(pageNumber, Convert.ToInt32(_PageSizeAppSettings.Size), GetTotalCount(orderList), "activeLink", Url.Action("neworders", "kitchendashboard"), "disableLink", string.Empty);
                return View(LoadOrders(orderList));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction("error", "home");
            }
        }

        /// <summary>
        /// PickupOrders
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        [Route(CommonConstants.RoutPickupOrders)]
        public IActionResult PickupOrders(int pageNumber, string search)
        {
            try
            {
                pageNumber = pageNumber == 0 ? 1 : pageNumber;
                ViewData["CurrentFilter"] = search;
                var input = LoadInput(pageNumber, search, "Out for delivery,Arrived,Ready");
                input.IsDeliver = 0;
                var orderList = _getKitchenDashboardQuery.KitchenManageOrdersPickupDelivery(input).Result;
                ViewBag.Paging = SetPaging.Set_Paging(pageNumber, Convert.ToInt32(_PageSizeAppSettings.Size), GetTotalCount(orderList), "activeLink", Url.Action("pickuporders", "kitchendashboard"), "disableLink", string.Empty);
                return View(LoadOrders(orderList));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction("error", "home");
            }
        }

        /// <summary>
        /// LoadPickupOrders
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult LoadPickupOrders(int pageNumber, string search)
        {
            pageNumber = pageNumber == 0 ? 1 : pageNumber;
            ViewData["CurrentFilter"] = search;
            var input = LoadInput(pageNumber, search, "Out for delivery,Arrived,Ready");
            input.IsDeliver = 0;
            var orderList = _getKitchenDashboardQuery.KitchenManageOrdersPickupDelivery(input).Result;
            return PartialView("_PickupOrders", LoadOrders(orderList));
        }

        /// <summary>
        /// DeliveryOrders
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        [Route(CommonConstants.RoutDeliveryOrders)]
        public IActionResult DeliveryOrders(int pageNumber, string search)
        {
            try
            {
                pageNumber = pageNumber == 0 ? 1 : pageNumber;
                ViewData["CurrentFilter"] = search;
                var input = LoadInput(pageNumber, search, "Out for delivery,Ready");
                input.IsDeliver = 1;
                var orderList = _getKitchenDashboardQuery.KitchenManageOrdersPickupDelivery(input).Result;
                ViewBag.Paging = SetPaging.Set_Paging(pageNumber, Convert.ToInt32(_PageSizeAppSettings.Size), GetTotalCount(orderList), "activeLink", Url.Action("deliveryorders", "kitchendashboard"), "disableLink", string.Empty);
                return View(LoadOrders(orderList));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction("error", "home");
            }
        }

        /// <summary>
        /// GetOrderInformationForPrint
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetOrderInformationForPrint(string orderId)
        {
            var response = _getKitchenDashboardQuery.KitchenOrderInvoice(Convert.ToInt64(_protector.Unprotect(orderId))).Result;
            var dishesResponse = _getKitchenDashboardQuery.KitchenOrderDishesInvoice(Convert.ToInt64(_protector.Unprotect(orderId))).Result;
            var model = new KitchenOrderInvoiceVm()
            {
                FoodOrderDetail = response,
                DishesItem = LoadDishesItem(dishesResponse)
            };

            return PartialView("_ReceiptPrint", model);
        }

        /// <summary>
        /// PreparationOrders
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public IActionResult PreparationOrders(int pageNumber, string search)
        {
            try
            {
                pageNumber = pageNumber == 0 ? 1 : pageNumber;
                ViewData["CurrentFilter"] = search;
                var input = LoadInput(pageNumber, search, "Preparing");
                var orderList = _getKitchenDashboardQuery.KitchenManageOrders(input).Result;
                ViewBag.Paging = SetPaging.Set_Paging(pageNumber, Convert.ToInt32(_PageSizeAppSettings.Size), GetTotalCount(orderList), "activeLink", Url.Action("preparationorders", "kitchendashboard"), "disableLink", string.Empty);
                return View(LoadOrders(orderList));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction("error", "home");
            }
        }

        /// <summary>
        /// ReadyOrders
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="search"></param>
        /// <returns></returns>

        [Route(CommonConstants.RoutPreparingOrders)]
        public IActionResult ReadyOrders(int pageNumber, string search)
        {
            try
            {
                pageNumber = pageNumber == 0 ? 1 : pageNumber;
                ViewData["CurrentFilter"] = search;
                var input = LoadInput(pageNumber, search, "Preparing");
                var orderList = _getKitchenDashboardQuery.KitchenManageOrders(input).Result;
                ViewBag.Paging = SetPaging.Set_Paging(pageNumber, Convert.ToInt32(_PageSizeAppSettings.Size), GetTotalCount(orderList), "activeLink", Url.Action("readyorders", "kitchendashboard"), "disableLink", string.Empty);
                return View(LoadOrders(orderList));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction("error", "home");
            }
        }

        /// <summary>
        /// CompletedOrders
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="search"></param>
        /// <returns></returns>

        [Route(CommonConstants.RoutCompletedOrders)]
        public IActionResult CompletedOrders(int pageNumber, string search)
        {
            try
            {
                int totalRecord = 0;
                pageNumber = pageNumber == 0 ? 1 : pageNumber;
                ViewData["CurrentFilter"] = search;

                var input = new KitchenInputDTO()
                {
                    UserId = Convert.ToInt64(_iClaimService.GetClaim(CommonConstants.SaavorUserId)),
                    Page = pageNumber,
                    Size = Convert.ToInt32(_PageSizeAppSettings.Size),
                    Search = search,
                    ProfileId = Convert.ToInt64(_iClaimService.GetClaim(CommonConstants.ProfileId)),
                    OrderType = "Delivered"
                };

                List<KitchenManageOrderVm> orderList = new List<KitchenManageOrderVm>();
                orderList = _getKitchenDashboardQuery.KitchenManageOrders(input).Result;
                List<KitchenOrdersVm> orders = new List<KitchenOrdersVm>();
                if (orderList != null && orderList.Count > 0)
                {
                    var res = orderList.Select(x => new
                    {
                        x.TotalRecord
                    }).FirstOrDefault();

                    totalRecord = res.TotalRecord;

                    orders = LoadCompletedOrders(orderList);

                }

                ViewBag.Paging = SetPaging.Set_Paging(pageNumber, Convert.ToInt32(_PageSizeAppSettings.Size), totalRecord, "activeLink", Url.Action("completedorders", "kitchendashboard"), "disableLink", string.Empty);
                return View(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction("error", "home");
            }
        }

        /// <summary>
        /// RejectedOrders
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        [Route(CommonConstants.RoutRejectedOrders)]
        public IActionResult RejectedOrders(int pageNumber, string search)
        {
            try
            {
                pageNumber = pageNumber == 0 ? 1 : pageNumber;
                ViewData["CurrentFilter"] = search;
                var input = LoadInput(pageNumber, search, "Rejected");
                var orderList = _getKitchenDashboardQuery.KitchenManageOrders(input).Result;
                ViewBag.Paging = SetPaging.Set_Paging(pageNumber, Convert.ToInt32(_PageSizeAppSettings.Size), GetTotalCount(orderList), "activeLink", Url.Action("rejectedorders", "kitchendashboard"), "disableLink", string.Empty);
                return View(LoadOrders(orderList));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction("error", "home");
            }
        }

        /// <summary>
        /// AcceptRejectOrder
        /// </summary>
        /// <param name="status"></param>
        /// <param name="id"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> AcceptRejectOrder(string status, string id, string reason, string systemDateTime, int IsDelivery)
        {
            try
            {
                bool res = true;
                string foodOrderIds = string.Empty;
                string[] ids = id.Split(",");
                if (ids.Length > 0)
                {
                    foreach (var item in ids)
                    {
                        if (!string.IsNullOrEmpty(foodOrderIds))
                            foodOrderIds = foodOrderIds + "," + _protector.Unprotect(item);
                        else
                            foodOrderIds = _protector.Unprotect(item);
                    }
                }
                if (Convert.ToString(status).ToLowerInvariant().Trim() == "accept" || Convert.ToString(status).ToLowerInvariant().Trim() == "accept all")
                {
                    var input = new FoodOrderStatusDTO()
                    {
                        ProfileId = Convert.ToInt64(_iClaimService.GetClaim(CommonConstants.ProfileId)),
                        FoodOrderIds = foodOrderIds,
                        OrderStatus = "Preparing",
                        SystemDateTime = systemDateTime,
                        RejectReason = reason
                    };
                    if (ids.Length > 0)
                    {
                        foreach (var item in ids)
                        {
                            res = await UpdateOrderStatusAcceptReject(input.OrderStatus, item, reason, systemDateTime, IsDelivery);
                        }
                    }
                    return Json(Convert.ToString(res));
                }
                else if (Convert.ToString(status).ToLowerInvariant().Trim() == "reject" || Convert.ToString(status).ToLowerInvariant().Trim() == "rejectall")
                {
                    var input = new FoodOrderStatusDTO()
                    {
                        ProfileId = Convert.ToInt64(_iClaimService.GetClaim(CommonConstants.ProfileId)),
                        FoodOrderIds = foodOrderIds,
                        OrderStatus = "Rejected",
                        SystemDateTime = systemDateTime,
                        RejectReason = reason
                    };
                    if (ids.Length > 0)
                    {
                        foreach (var item in ids)
                        {
                            res = await UpdateOrderStatusAcceptReject(input.OrderStatus, item, reason, systemDateTime,IsDelivery);
                        }
                    }
                    //var result = UpdateOrderStatus(input);
                    return Json(Convert.ToString(res).ToLowerInvariant());
                }
                else if (Convert.ToString(status).ToLowerInvariant().Trim() == "completed")
                    status = "Delivered";
                else if (Convert.ToString(status).ToLowerInvariant().Trim() == "start preparing")
                    status = "Preparing";
                else if (Convert.ToString(status).ToLowerInvariant().Trim() == "ready to deliver" || Convert.ToString(status).ToLowerInvariant().Trim() == "ready for pickup")
                    status = "Ready";
                // status = "Out for delivery";
                else if (Convert.ToString(status).ToLowerInvariant().Trim() == "deliver")
                    status = "Delivered";

                if (string.IsNullOrEmpty(reason))
                {
                    reason = string.Empty;
                }
                else
                {
                    reason = reason.Replace("'", "");
                }
                if (ids.Length > 0)
                {
                    foreach (var item in ids)
                    {
                        res =  await UpdateOrderStatus(status, item, reason, systemDateTime);
                    }
                }
                return Json(Convert.ToString(res));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Json("false");
            }
        }

        /// <summary>
        /// UpdateOrderStatusAcceptReject
        /// </summary>
        /// <param name="status"></param>
        /// <param name="orderId"></param>
        /// <param name="reason"></param>
        /// <param name="systemDateTime"></param>
        /// <returns></returns>
        private async System.Threading.Tasks.Task<bool> UpdateOrderStatusAcceptReject(string status, string orderId, string reason, string systemDateTime,int isDelivery)
        {
            var orderStatusDTO = new OrderStatusAcceptRejectDTO()
            {
                OrderStatus = status,
                OrderId = Convert.ToInt64(_protector.Unprotect(orderId)),
                ProfileId = Convert.ToInt64(_iClaimService.GetClaim(CommonConstants.ProfileId)),
                CurrentDate = systemDateTime,
                DeliveryBy = isDelivery > 0 ? "Kitchen":string.Empty,
                RejectReason = reason
            };
            var content = JsonConvert.SerializeObject(orderStatusDTO);
            var httpResponse = await _client.PostAsync(_appUsers.AcceptRejectOrder, new StringContent(content, Encoding.Default, "application/json"));
            httpResponse.EnsureSuccessStatusCode();
            var readResponse = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ResponseDTO>(readResponse);

            if (response != null && response.ReturnCode == "1")
                return true;

            return false;
        }

        /// <summary>
        /// Method is used to update the order status except Accept and Reject.
        /// </summary>
        /// <param name="status"></param>
        /// <param name="orderId"></param>
        /// <param name="reason"></param>
        /// <param name="systemDateTime"></param>
        /// <returns></returns>
        private async System.Threading.Tasks.Task<bool> UpdateOrderStatus(string status, string orderId, string reason, string systemDateTime)
        {
            var orderStatusDTO = new OrderStatusDTO()
            {
                OrderStatus = status,
                OrderId = Convert.ToInt64(_protector.Unprotect(orderId)),
                ProfileId = Convert.ToInt64(_iClaimService.GetClaim(CommonConstants.ProfileId)),
                CurrentDate = systemDateTime
            };
            var content = JsonConvert.SerializeObject(orderStatusDTO);
            var httpResponse = await _client.PostAsync(_appUsers.OrderStatusUpdate, new StringContent(content, Encoding.Default, "application/json"));
            httpResponse.EnsureSuccessStatusCode();
            var readResponse = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ResponseDTO>(readResponse);

            if (response != null && response.ReturnCode == "1")
                return true;

            return false;
        }

        /// <summary>
        /// UpdateOrderStatus
        /// </summary>
        /// <param name="inputDTO"></param>
        /// <returns></returns>
        private ActionResult<Response> UpdateOrderStatus(FoodOrderStatusDTO inputDTO)
        {
            return _getKitchenDashboardQuery.UpdateOrderStatus(inputDTO).Result;
        }

        /// <summary>
        /// GetOrderInvoice
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetOrderInvoice(string orderId)
        {
            var response = _getKitchenDashboardQuery.KitchenOrderInvoice(Convert.ToInt64(_protector.Unprotect(orderId))).Result;
            var dishesResponse = _getKitchenDashboardQuery.KitchenOrderDishesInvoice(Convert.ToInt64(_protector.Unprotect(orderId))).Result;
            var model = new KitchenOrderInvoiceVm()
            {
                FoodOrderDetail = response,
                DishesItem = LoadDishesItem(dishesResponse)
            };

            return PartialView("_OrderInvoice", model);
        }

        /// <summary>
        /// GetOrderInvoiceToPDF
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetOrderInvoiceToPDF(string orderId)
        {
            var response = _getKitchenDashboardQuery.KitchenOrderInvoice(Convert.ToInt64(_protector.Unprotect(orderId))).Result;
            var dishesResponse = _getKitchenDashboardQuery.KitchenOrderDishesInvoice(Convert.ToInt64(_protector.Unprotect(orderId))).Result;
            var model = new KitchenOrderInvoiceVm()
            {
                FoodOrderDetail = response,
                DishesItem = LoadDishesItem(dishesResponse)
            };

            return PartialView("_OrderInvoiceToPDF", model);
        }

        /// <summary>
        /// GetExportOrders
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetExportOrders(string search)
        {
            var input = new KitchenInputDTO()
            {
                UserId = Convert.ToInt64(_iClaimService.GetClaim(CommonConstants.SaavorUserId)),
                ProfileId = Convert.ToInt64(_iClaimService.GetClaim(CommonConstants.ProfileId)),
                //ProfileId = 1478,
                Search = search,
            };

            List<KitchenManageOrdersReportVm> orderReportList = new List<KitchenManageOrdersReportVm>();
            orderReportList = _getKitchenDashboardQuery.KitchenManageOrdersReportToExport(input).Result;

            List<KitchenOrdersReportVm> _list = new List<KitchenOrdersReportVm>();
            if (orderReportList != null && orderReportList.Count > 0)
            {
                _list = orderReportList.Select(item => new KitchenOrdersReportVm
                {
                    Id = item.Id,
                    OrderId = item.OrderId,
                    OrderDate = item.OrderDate,
                    UserName = item.UserName,
                    OrderStatus = item.OrderStatus,
                    DishOrders = item.DishOrders,
                    AddressLine = item.AddressLine,
                    OrderDishes = GetOrderDishes(item.DishOrders)
                }).ToList();

            }
            return PartialView("_OrdersReport", _list);
        }

        /// <summary>
        /// GenerateInvoicePDF
        /// </summary>
        /// <param name="data"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GenerateInvoicePDF(string data, string orderId)
        {
            string outputPDF = Path.Combine(_environment.WebRootPath, "pdf");
            string pdfName = "/invoice_" + orderId + ".pdf";
            HtmlConverter.ConvertToPdf(data, new FileStream(outputPDF + pdfName, FileMode.Create));
            return Json("/pdf" + pdfName);
        }

        /// <summary>
        /// DeleteInvoicePDF
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteInvoicePDF(string fileName)
        {
            string path = Path.Combine(_environment.WebRootPath, "pdf");
            fileName = fileName.Replace("/pdf", "");
            if (System.IO.File.Exists(path + fileName))
            {
                System.IO.File.Delete(path + fileName);
            }
            return Json(string.Empty);
        }

        /// <summary>
        /// GetOrdersCount
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetOrdersCount()
        {
            try
            {
                return Json(_getKitchenDashboardQuery.GetOrdersCount(Convert.ToInt64(_iClaimService.GetClaim(CommonConstants.ProfileId))).Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Json(string.Empty);
            }
        }

        #endregion

        #region Reports and Reviews

        /// <summary>
        /// ManageReports
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        //[Route(CommonConstants.RoutOrderReport)]
        public IActionResult ManageReports(int pageNumber, string search)
        {
            try
            {
                int totalRecord = 0;
                pageNumber = pageNumber == 0 ? 1 : pageNumber;

                if (!string.IsNullOrEmpty(search))
                    TempData["CalendarDate"] = search;
                else
                {
                    search = DateTime.Now.Date.ToString("MM-dd-yyyy");
                    TempData["CalendarDate"] = search;
                }

                var input = new KitchenInputDTO()
                {
                    UserId = Convert.ToInt64(_iClaimService.GetClaim(CommonConstants.SaavorUserId)),
                    Page = pageNumber,
                    Size = Convert.ToInt32(_PageSizeAppSettings.Size),
                    Search = search,
                    ProfileId = Convert.ToInt64(_iClaimService.GetClaim(CommonConstants.ProfileId)),
                    //ProfileId = 1478,
                    OrderType = "Pending,Preparation"
                };

                List<KitchenManageOrdersReportVm> orderReportList = new List<KitchenManageOrdersReportVm>();
                orderReportList = _getKitchenDashboardQuery.KitchenManageOrdersReport(input).Result;

                List<KitchenOrdersReportVm> _list = new List<KitchenOrdersReportVm>();
                if (orderReportList != null && orderReportList.Count > 0)
                {
                    var res = orderReportList.Select(x => new
                    {
                        x.TotalRecord
                    }).FirstOrDefault();
                    totalRecord = res.TotalRecord;

                    _list = orderReportList.Select(item => new KitchenOrdersReportVm
                    {
                        Id = item.Id,
                        OrderId = item.OrderId,
                        OrderDate = item.OrderDate,
                        UserName = item.UserName,
                        OrderStatus = item.OrderStatus,
                        DishOrders = item.DishOrders,
                        AddressLine = item.AddressLine,
                        OrderDishes = GetOrderDishes(item.DishOrders)
                    }).ToList();

                }
                ViewBag.Paging = SetPaging.Set_Paging(pageNumber, Convert.ToInt32(_PageSizeAppSettings.Size), totalRecord, "activeLink", Url.Action("managereports", "kitchendashboard"), "disableLink", search);
                return View(_list);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction("error", "home");
            }
        }

        /// <summary>
        /// ReviewFeedbacks
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public IActionResult ReviewFeedbacks(string fromDate, string toDate, int pageNumber)
        {
            try
            {
                if (!string.IsNullOrEmpty(fromDate))
                    TempData["CalendarDateFrom"] = fromDate;
                else
                {
                    fromDate = DateTime.Now.Date.ToString("MM-dd-yyyy");
                    TempData["CalendarDateFrom"] = fromDate;
                }

                if (!string.IsNullOrEmpty(toDate))
                    TempData["CalendarDateTo"] = toDate;
                else
                {
                    toDate = DateTime.Now.Date.ToString("MM-dd-yyyy");
                    TempData["CalendarDateTo"] = toDate;
                }
                pageNumber = pageNumber == 0 ? 1 : pageNumber;
                var input = new KitchenInputDTO()
                {
                    ProfileId = Convert.ToInt64(_iClaimService.GetClaim(CommonConstants.ProfileId)),
                    FromDate = fromDate,
                    ToDate = toDate,
                    Page = pageNumber,
                    Size = Convert.ToInt32(_PageSizeAppSettings.Size)
                };

                int totalRecord = 0;
                List<KitchenReviewsVm> kitchenReviews = new List<KitchenReviewsVm>();
                kitchenReviews = _getKitchenDashboardQuery.KitchenReviews(input).Result;

                if (kitchenReviews != null && kitchenReviews.Count > 0)
                {
                    var res = kitchenReviews.Select(x => new
                    {
                        x.AverageRating,
                        x.TotalRecord
                    }).FirstOrDefault();

                    ViewBag.AvarageRating = res.AverageRating;
                    int totalStars = 5;
                    decimal d = Convert.ToDecimal(res.AverageRating);
                    int stars = totalStars - Convert.ToInt32(d);
                    ViewBag.GrayStar = Convert.ToString(stars);
                    totalRecord = res.TotalRecord;
                }
                List<KitchenReviewsModel> reviews = LoadReviewReply(kitchenReviews);
                ViewBag.Paging = SetPaging.Set_Paging_Dates(pageNumber, Convert.ToInt32(_PageSizeAppSettings.Size), totalRecord, "activeLink", Url.Action("reviewfeedbacks", "kitchendashboard"), "disableLink", fromDate, toDate);
                return View(reviews);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction("error", "home");
            }
        }

        /// <summary>
        /// ReviewReply
        /// </summary>
        /// <param name="reply"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ReviewReply(string reply, string id)
        {
            return Json(_getKitchenDashboardQuery.KitchenReviewReply(reply, Convert.ToInt64(id)).Result);
        }

        /// <summary>
        /// DeleteKitchenReview
        /// </summary>
        /// <param name="reviewId"></param>
        /// <returns></returns>

        [HttpPost]
        public JsonResult DeleteKitchenReview(string reviewId)
        {
            try
            {
                var input = new saavor.Shared.DTO.Kitchen.KitchenInputDTO()
                {
                    ReviewId = Convert.ToInt64(reviewId)
                };
                return Json(_getKitchenDashboardQuery.KitchenReviewDelete(input));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Json("0");
            }
        }

        /// <summary>
        /// GenerateInvoicePDF
        /// </summary>
        /// <param name="data"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>

        #endregion

        #region Refund

        [Route(CommonConstants.RoutRefund)]
        /// <summary>
        /// Refunds
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public IActionResult Refunds(int pageNumber, string search)
        {
            try
            {
                int totalRecord = 0;
                pageNumber = pageNumber == 0 ? 1 : pageNumber;
                ViewData["CurrentFilter"] = search;

                var input = new KitchenInputDTO()
                {
                    UserId = Convert.ToInt64(_iClaimService.GetClaim(CommonConstants.SaavorUserId)),
                    Page = pageNumber,
                    Size = Convert.ToInt32(_PageSizeAppSettings.Size),
                    Search = search,
                    ProfileId = Convert.ToInt64(_iClaimService.GetClaim(CommonConstants.ProfileId)),
                    OrderType = "Delivered"
                };

                List<KitchenManageOrderVm> orderList = new List<KitchenManageOrderVm>();
                orderList = _getKitchenDashboardQuery.KitchenManageOrders(input).Result;

                List<KitchenOrdersVm> orders = new List<KitchenOrdersVm>();
                if (orderList != null && orderList.Count > 0)
                {
                    var res = orderList.Select(x => new
                    {
                        x.TotalRecord
                    }).FirstOrDefault();

                    totalRecord = res.TotalRecord;

                    orders = LoadCompletedOrders(orderList);
                }
                ViewBag.Paging = SetPaging.Set_Paging(pageNumber, Convert.ToInt32(_PageSizeAppSettings.Size), totalRecord, "activeLink", Url.Action("refunds", "kitchendashboard"), "disableLink", string.Empty);
                return View(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction("error", "home");
            }
        }

        /// <summary>
        /// GetItemToRefund
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetItemToRefund(string orderId)
        {
            var items = _getKitchenDashboardQuery.GetItemRefunds(Convert.ToInt64(_protector.Unprotect(orderId))).Result;
            decimal orderAmount = 0;
            string card = string.Empty;
            string foodOrderId = string.Empty;
            if (items != null && items.Count > 0)
            {
                var res = items.Select(x => new
                {
                    x.Total,
                    x.Card,
                    x.FoodOrderId
                }).FirstOrDefault();
                orderAmount = Convert.ToDecimal(res.Total);
                card = res.Card;
                foodOrderId = UtilitieService.Encrypt(Convert.ToString(res.FoodOrderId));
            }
            var model = new OrderRefundVm()
            {
                FoodOrderId = foodOrderId,
                Card = card,
                OrderAmount = Math.Round(orderAmount, 2),
                DishesItem = LoadDishes(items)
            };
            TempData["TotalOrderAmount"] = Convert.ToString(model.OrderAmount);
            return PartialView("_Refund", model);
        }

        /// <summary>
        /// GetItemDetailToRefund
        /// </summary>
        /// <param name="foodOrderDetailId"></param>
        /// <param name="qty"></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        public JsonResult GetItemDetailToRefund(string foodOrderDetailId, string qty)
        {
            var items = _getKitchenDashboardQuery.GetItemDetail(Convert.ToInt64(foodOrderDetailId)).Result;
            if (items != null)
            {
                decimal totalPrice = Convert.ToDecimal(items.Price);
                decimal pricePerItem = Convert.ToDecimal(items.PricePerItem);
                decimal price = Math.Round(totalPrice * Convert.ToInt32(qty), 2);
                return Json(Convert.ToString(price));
            }
            return Json("0.00");
        }
        /// <summary>
        /// ProcessRefund
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> ProcessRefundAsync(string foodOrderId, string refundAmount, string refundReason, string lineItem)
        {
            decimal orderAmount = Convert.ToDecimal(TempData["TotalOrderAmount"]);
            if (Convert.ToDecimal(refundAmount) <= orderAmount)
            {
                var input = new RefundItemInput()
                {
                    RefundAmount = Convert.ToDecimal(refundAmount),
                    RefundReason = refundReason,
                    FoodOrderId = foodOrderId,
                    ProfileId = Convert.ToInt32(_iClaimService.GetClaim(CommonConstants.ProfileId)),
                    LineItems = lineItem,
                    CurrentDate = string.Empty
                };
                var content = JsonConvert.SerializeObject(input);
                var httpResponse = await _client.PostAsync(_appUsers.RefundURL, new StringContent(content, Encoding.Default, "application/json"));

                httpResponse.EnsureSuccessStatusCode();
                var readResponse = await httpResponse.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<ResponseDTO>(readResponse);

                if (response != null && response.ReturnCode == "1")
                    return Json("1");
            }

            return Json("0");
        }
        #endregion

        #region Load input & data

        /// <summary>
        /// LoadInput
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="search"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        private KitchenInputDTO LoadInput(int pageNumber, string search, string orderType)
        {
            return new KitchenInputDTO()
            {
                UserId = Convert.ToInt64(_iClaimService.GetClaim(CommonConstants.SaavorUserId)),
                Page = pageNumber,
                Size = Convert.ToInt32(_PageSizeAppSettings.Size),
                Search = search,
                ProfileId = Convert.ToInt64(_iClaimService.GetClaim(CommonConstants.ProfileId)),
                OrderType = orderType
            };
        }

        /// <summary>
        /// LoadOrders
        /// </summary>
        /// <param name="inputOrders"></param>
        /// <returns></returns>
        private List<KitchenOrdersVm> LoadOrders(List<KitchenManageOrderVm> inputOrders)
        {
            List<KitchenOrdersVm> outputOrders = new List<KitchenOrdersVm>();

            if (inputOrders != null && inputOrders.Count > 0)
            {
                outputOrders = inputOrders.Select(item => new KitchenOrdersVm
                {
                    Id = item.Id,
                    OrderId = item.OrderId,
                    OrderDate = item.OrderDate,
                    UserName = item.UserName,
                    UserEmail = item.UserEmail,
                    OrderStatus = item.OrderStatus,
                    IsDelivery = item.IsDelivery,
                    FoodOrderId = _protector.Protect(Convert.ToString(item.Id)),
                    OrderDishes = GetOrderDishes(item.DishOrders),
                    RejectReason = item.RejectReason
                }).ToList();
            }
            return outputOrders;
        }

        /// <summary>
        /// GetTotalCount
        /// </summary>
        /// <param name="inputOrders"></param>
        /// <returns></returns>
        private static int GetTotalCount(List<KitchenManageOrderVm> inputOrders)
        {
            if (inputOrders != null && inputOrders.Count > 0)
            {
                var res = inputOrders.Select(x => new
                {
                    x.TotalRecord
                }).FirstOrDefault();
                return res.TotalRecord;
            }

            return 0;
        }

        /// <summary>
        /// GetOrderDishes
        /// </summary>
        /// <param name="orderDishes"></param>
        /// <returns></returns>
        private static List<KitchenOrderDishesVm> GetOrderDishes(string orderDishes)
        {
            List<KitchenOrderDishesVm> list = new List<KitchenOrderDishesVm>();
            if (!string.IsNullOrEmpty(orderDishes))
            {
                string[] multipleDish = orderDishes.Split(",");
                if (multipleDish.Count() > 0)
                {
                    foreach (var item in multipleDish)
                    {
                        if (item.Contains("~"))
                        {
                            string[] singleDish = item.Split("~");
                            if (singleDish.Count() > 0)
                            {
                                list.Add(new KitchenOrderDishesVm
                                {
                                    Quantity = Convert.ToString(singleDish[0]),
                                    DishName = Convert.ToString(singleDish[1])
                                });
                            }
                        }
                        else
                        {
                            list.Add(new KitchenOrderDishesVm
                            {
                                Quantity = string.Empty,
                                DishName = Convert.ToString(item)
                            });
                        }
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// LoadReviewReply
        /// </summary>
        /// <param name="inputOrders"></param>
        /// <returns></returns>
        private List<KitchenReviewsModel> LoadReviewReply(List<KitchenReviewsVm> inputOrders)
        {
            List<KitchenReviewsModel> outputReview = new List<KitchenReviewsModel>();

            if (inputOrders != null && inputOrders.Count > 0)
            {
                outputReview = inputOrders.Select(item => new KitchenReviewsModel
                {
                    ReviewId = item.ReviewId,
                    Stars = item.Stars,
                    ReviewMessage = item.ReviewMessage,
                    FirstName = item.FirstName,
                    Lastname = item.Lastname,
                    CreateDate = item.CreateDate,
                    AverageRating = item.AverageRating,
                    ProfileImg = LoadReviewerProfile(string.Format("{0}{1}", !(string.IsNullOrEmpty(item.ProfileImgPath)) ? _appUsers.SaavorUser : string.Empty, item.ProfileImgPath)) ? string.Format("{0}{1}", _appUsers.ProfileImage, item.ProfileImgPath) : _appUsers.DefaultUserImage,
                    ReplyReviewList = _getKitchenDashboardQuery.KitchenReviewReplyGet(item.ReviewId).Result
                }).ToList();
            }

            return outputReview;
        }

        /// <summary>
        /// 
        
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool LoadReviewerProfile(string path)
        {
            if (string.IsNullOrEmpty(path) && !Directory.Exists(path))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// LoadCompletedOrders
        /// </summary>
        /// <param name="inputOrders"></param>
        /// <returns></returns>
        private List<KitchenOrdersVm> LoadCompletedOrders(List<KitchenManageOrderVm> inputOrders)
        {
            List<KitchenOrdersVm> outputOrders = new List<KitchenOrdersVm>();

            if (inputOrders != null && inputOrders.Count > 0)
            {
                outputOrders = inputOrders.Select(item => new KitchenOrdersVm
                {
                    Id = item.Id,
                    OrderId = item.OrderId,
                    OrderDate = item.OrderDate,
                    UserName = item.UserName,
                    UserEmail = item.UserEmail,
                    OrderStatus = item.OrderStatus,
                    FoodOrderId = _protector.Protect(Convert.ToString(item.Id)),
                    OrderDishes = GetOrderDishes(item.DishOrders)
                }).ToList();
            }
            return outputOrders;
        }

        /// <summary>
        /// LoadDishes
        /// </summary>
        /// <param name="inputOrders"></param>
        /// <returns></returns>
        private List<OrderItemModel> LoadDishes(List<OrderItemVm> inputOrders)
        {
            List<OrderItemModel> outputOrders = new List<OrderItemModel>();

            if (inputOrders != null && inputOrders.Count > 0)
            {
                outputOrders = inputOrders.Select(item => new OrderItemModel
                {
                    DishId = item.DishId,
                    DishName = item.DishName,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    TotalQuantity = item.TotalQuantity,
                    Total = item.Total,
                    Card = item.Card,
                    FoodOrderId = item.FoodOrderId,
                    FoodOrderDetailId = item.FoodOrderDetailId,
                    LineItem = item.LineItem,
                    DishAddOns = LoadAddOns(item.LineItem),
                    AlreadyRefunded = item.AlreadyRefunded
                }).ToList();
            }
            return outputOrders;
        }

        /// <summary>
        /// LoadAddOns
        /// </summary>
        /// <param name="inputOrders"></param>
        /// <returns></returns>
        private List<DishAddOns> LoadAddOns(string input)
        {
            List<DishAddOns> outputOrders = new List<DishAddOns>();
            if (!string.IsNullOrEmpty(input))
            {
                // input = "RANCH DRESSING-1~ITALAIN DRESSING-2~ITALAIN DRESSING-3 |0.10~0.15~0.20";
                string[] lineItems = input.Split("|");
                //0,1
                if (lineItems.Length > 0)
                {
                    string[] items = lineItems[0].Split("~");
                    string[] costs = lineItems[1].Split("~");
                    if (items.Length > 0)
                    {
                        for (int i = 0; i < items.Length; i++)
                        {

                            outputOrders.Add(new DishAddOns
                            {
                                ItemName = items[i],
                                ItemCost = costs[i]
                            });
                        }
                    }
                }
            }


            return outputOrders;
        }

        /// <summary>
        /// LoadDishesItem
        /// </summary>
        /// <param name="inputItems">List<KitchenOrderDishesItemVm></param>
        /// <returns>List<KitchenOrderDishesItemVm></returns>
        private List<KitchenOrderDishesItemVm> LoadDishesItem(List<KitchenOrderDishesItemVm> inputItems)
        {
            List<KitchenOrderDishesItemVm> outPutItems = new List<KitchenOrderDishesItemVm>();

            if (inputItems != null && inputItems.Count > 0)
            {
                outPutItems = inputItems.Select(item => new KitchenOrderDishesItemVm
                {
                    DishId = item.DishId,
                    DishName = item.DishName,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    Total = Convert.ToDecimal(Convert.ToDecimal(item.Price) * Convert.ToInt32(item.Quantity))
                }).ToList();
            }
            return outPutItems;
        }

        /// <summary>
        /// ReturnMonthYear
        /// </summary>
        /// <param name="date"></param>
        /// <param name="isMonth"></param>
        /// <returns></returns>
        private static int ReturnMonthYear(string date, bool isMonth)
        {
            if(!string.IsNullOrEmpty(date))
            {
                string[] array = date.Split("-");
                if(array.Length > 0)
                {
                    return Convert.ToInt32(array[isMonth ? 0 : 1]);
                }
            }
            return Convert.ToInt32(isMonth ? DateTime.Now.Month : DateTime.Now.Year);
        }

        private static string ReturnDate(int monthDays,string monthYear)
        {
            string[] array = monthYear.Split("-");
            string date = string.Empty;
            if (array.Length > 0)
            {
                date = array[1] + "-" + array[0] + "-" + Convert.ToString(monthDays);
               
            }
            return date;
        }

       
        #endregion
    }
}
