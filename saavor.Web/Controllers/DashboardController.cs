using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using saavor.Application.Kitchen.Query;
using saavor.Shared.AppSettings;
using saavor.Shared.Constants;
using saavor.Shared.Filter;
using saavor.Shared.Interfaces;
using saavor.Shared.ViewModel;
using System;
using System.Linq;

namespace saavor.Web.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IClaimService _iClaimService;
        private readonly GetKitchenDashboardQuery _getKitchenDashboardQuery;
        private readonly IDataProtector _protector;
        private readonly DataProtection _dataProtectionAppSettings;
        private readonly PageSize _PageSizeAppSettings;
        public DashboardController(IClaimService iClaimServiceInstance, GetKitchenDashboardQuery getKitchenDashboardQueryInstance
            , IOptions<DataProtection> dataProtectionAppSettingsInstacne, IDataProtectionProvider providerInstance, IOptions<PageSize> pageSizeAppSettingsInstacne)
        {
            _iClaimService = iClaimServiceInstance;
            _getKitchenDashboardQuery = getKitchenDashboardQueryInstance;
            _dataProtectionAppSettings = dataProtectionAppSettingsInstacne.Value;
            _protector = providerInstance.CreateProtector(_dataProtectionAppSettings.Key);
            _PageSizeAppSettings = pageSizeAppSettingsInstacne.Value;
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public IActionResult Home(int pageNumber, string search)
        {
            int totalRecord = 0;
            Int64 kitchenId = 0;
            pageNumber = pageNumber == 0 ? 1 : pageNumber;
            ViewData["CurrentFilter"] = search;
            if (!(Int64.TryParse(search, out kitchenId)))
            {
                kitchenId = 0;
            }
            var input = new saavor.Shared.DTO.Kitchen.KitchenInputDTO()
            {
                UserId = Convert.ToInt64(_iClaimService.GetClaim(CommonConstants.SaavorUserId)),
                Page = pageNumber,
                Size = Convert.ToInt32(_PageSizeAppSettings.Size),
                KitchenId = kitchenId,
                ProfileId = 0
            };

            var model = new KitchenDashboardVm()
            {
                Count = _getKitchenDashboardQuery.KitchenDashboardCount(input).Result,
                Kitchen = _getKitchenDashboardQuery.KitchenDashboardKitchens(input).Result
            };
            if (model != null && model.Kitchen != null && model.Kitchen.Count > 0)
            {
                var res = model.Kitchen.Select(x => new
                {
                    x.TotalRecord
                }).FirstOrDefault();

                totalRecord = res.TotalRecord;
                model.Kitchen = model.Kitchen.Select(x =>
                {
                    x.ProtectedProfileId = _protector.Protect(Convert.ToString(x.ProfileId));
                    return x;
                }).ToList();
            }
            ViewBag.Paging = SetPaging.Set_Paging(pageNumber, 10, totalRecord, "activeLink", Url.Action("overview", "dashboard"), "disableLink", string.Empty);
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetRevenueChart()
        {
            try
            {
                var input = new saavor.Shared.DTO.Kitchen.KitchenInputDTO()
                {
                    UserId = Convert.ToInt64(_iClaimService.GetClaim(CommonConstants.SaavorUserId))
                };
                return Json(_getKitchenDashboardQuery.KitchenRevenueChart(input).Result);
            }
            catch
            {
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
                var input = new saavor.Shared.DTO.Kitchen.KitchenInputDTO()
                {
                    UserId = Convert.ToInt64(_iClaimService.GetClaim(CommonConstants.SaavorUserId)),
                    Type = type ?? "Monthly"
                };
                var orderSummaryChart = _getKitchenDashboardQuery.KitchenOrderSummaryChart(input).Result;
                return Json(orderSummaryChart);
            }
            catch
            {
                return Json(new KitchenOrderSummaryVm());
            }
        }

        [HttpPost]
        public JsonResult AutoCompleteKitchen(string search)
        {
            try
            {
                var input = new saavor.Shared.DTO.Kitchen.KitchenInputDTO()
                {
                    UserId = Convert.ToInt64(_iClaimService.GetClaim(CommonConstants.SaavorUserId)),
                    Search = search
                };
                return Json(_getKitchenDashboardQuery.KitchenAutoComplete(input));
            }
            catch
            {
                return Json(new KitchenAutoCompleteVm());
            }
        }
        [HttpPost]
        public JsonResult DeleteKitchen(string kitchenId)
        {
            try
            {
                var input = new saavor.Shared.DTO.Kitchen.KitchenInputDTO()
                {
                    UserId = Convert.ToInt64(_iClaimService.GetClaim(CommonConstants.SaavorUserId)),
                    ProfileId = Convert.ToInt64(_protector.Unprotect(Convert.ToString(kitchenId))),
                };
                return Json(_getKitchenDashboardQuery.KitchenDelete(input));
            }
            catch
            {
                return Json("0");
            }
        }

    }
}
