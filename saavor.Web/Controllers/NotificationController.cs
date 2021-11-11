using System; 
using Microsoft.AspNetCore.Mvc;
using saavor.Application.Notification.Query; 
using saavor.Shared.Constants;
using saavor.Shared.Interfaces;
using saavor.Shared.ViewModel.NotificationVm;
using nsNotification = saavor.Shared.DTO.Notification;
namespace saavor.Web.Controllers
{
    /// <summary>
    /// NotificationController
    /// </summary>
  
    public class NotificationController : Controller
    {
        /// <summary>
        /// claimService
        /// </summary>
        private readonly IClaimService claimService;

        /// <summary>
        /// notificationQuery
        /// </summary>
        private readonly NotificationQuery notificationQuery;

        /// <summary>
        /// NotificationController
        /// </summary>
        /// <param name="claimServiceInstance"></param>
        /// <param name="notificationQueryInstance"></param>
        public NotificationController(IClaimService claimServiceInstance
                                        , NotificationQuery notificationQueryInstance)
        {
            claimService = claimServiceInstance;
            notificationQuery = notificationQueryInstance;
        }

        /// <summary>
        /// OrderNotification
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult OrderNotification()
        {
            try
            {
                var input = new nsNotification.NotificationInputDTO()
                {
                    UserId = Convert.ToInt64(claimService.GetClaim(CommonConstants.SaavorUserId)),
                    ProfileId = Convert.ToInt64(claimService.GetClaim(CommonConstants.ProfileId)),
                };
                var response = notificationQuery.GetNotification(input).Result;
                if(response != null)
                {
                    var orders = new NotificationVm()
                    {
                        Count = response.Count,
                        Orders = response
                    };
                    return Json(orders);
                }
                return Json(new NotificationVm());
            }
            catch
            {
                return Json(new NotificationVm());
            }
        }
    }
}
