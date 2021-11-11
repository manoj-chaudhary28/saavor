using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using saavor.Application.Kitchen.Query;
using saavor.Shared.AppSettings;
using saavor.Shared.Constants;
using saavor.Shared.DTO.Kitchen;
using saavor.Shared.Enumrations;
using saavor.Shared.Filter;
using saavor.Shared.Interfaces;
using saavor.Shared.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saavor.Web.Controllers
{
    [Authorize]
    public class KitchenController : Controller
    {
        private readonly GetKitchenQuery _getKitchenQuery;
        private readonly IClaimService _iClaimService;
        private readonly IUpsertKitchenRequestCommand iUpsertKitchenRequestCommand;
        private readonly PageSize _PageSizeAppSettings;
        public KitchenController(GetKitchenQuery getKitchenQueryInstance, IClaimService iClaimServiceInstance,
                                   IOptions<PageSize> pageSizeAppSettingsInstacne, IUpsertKitchenRequestCommand iUpsertKitchenRequestCommandInstance)
        {
            _getKitchenQuery = getKitchenQueryInstance;
            _iClaimService = iClaimServiceInstance;
            iUpsertKitchenRequestCommand = iUpsertKitchenRequestCommandInstance;
            _PageSizeAppSettings = pageSizeAppSettingsInstacne.Value;
        }
        public async Task<IActionResult> Overview(string currentFilter, string search, int pageNumber, string pageSize)
        {

            try
            {
                int totalRecord = 0;
                Int64 kitchenId = 0;
                pageNumber = pageNumber == 0 ? 1 : pageNumber;
                ViewData["CurrentFilter"] = search;
                if (!(Int64.TryParse(search, out kitchenId)))
                {
                    kitchenId = 0;
                }
                List<KitchenDTO> kitchenList = _getKitchenQuery.GetKitchen(Convert.ToInt64(_iClaimService.GetClaim(CommonConstants.SaavorUserId)), kitchenId, pageNumber, Convert.ToInt32(_PageSizeAppSettings.Size)).Result;

                if (kitchenList != null && kitchenList.Count > 0)
                {
                    var res = kitchenList.Select(x => new
                    {
                        x.TotalRecord
                    }).FirstOrDefault();
                    totalRecord = res.TotalRecord;
                }

                var model = new KitchenVm()
                {
                    Kitchen = kitchenList
                };
                ViewBag.Paging = SetPaging.Set_Paging(pageNumber, Convert.ToInt32(_PageSizeAppSettings.Size), totalRecord, "activeLink", Url.Action("overview", "kitchen"), "disableLink", string.Empty);
                return await Task.Run(() => View(model));
            }
            catch 
            {
                return await Task.Run(() => View(new KitchenVm()));
            }

        }
        [HttpPost]
        public JsonResult RequestToKitchen(int kitchenId, int isChecked)
        {
            try
            {
                var input = new KitchenRequestDTO()
                {
                    ProfileId = Convert.ToInt32(kitchenId),
                    UserId = Convert.ToInt64(_iClaimService.GetClaim(CommonConstants.SaavorUserId)),
                    Status = isChecked > 0 ? KitchenRequestStatusEnum.Pending : KitchenRequestStatusEnum.Cancelled,
                    CreateDate = DateTime.UtcNow
                };
                var kitchenes = iUpsertKitchenRequestCommand.KitchenRequest(input);
                return Json("1");
            }
            catch
            {
                return Json("0");
            }
        }
        [HttpPost]
        public JsonResult AutoCompleteKitchen(string kitchenId)
        {
            try
            {
                return Json(_getKitchenQuery.GetKitchenAutoComplete(Convert.ToInt64(_iClaimService.GetClaim(CommonConstants.SaavorUserId)),kitchenId));
            }
            catch
            {
                return Json(new KitchenDTO());
            }
        }
    }
}
