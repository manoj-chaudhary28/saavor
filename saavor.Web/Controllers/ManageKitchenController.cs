using Microsoft.AspNetCore.Mvc;
using saavor.Shared.Constants;

namespace saavor.Web.Controllers
{
    /// <summary>
    /// Manage Kitchen
    /// </summary>
    public class ManageKitchenController : Controller
    {
        /// <summary>
        /// Manage Kitchen
        /// </summary>
        /// <returns></returns>
        [Route(CommonConstants.RoutManageKitchen)]
        public IActionResult ManageKitchen()
        {
            return View();
        }

        /// <summary>
        /// Manage Cuisines
        /// </summary>
        /// <returns></returns>
        [Route(CommonConstants.RoutManageCuisines)]
        public IActionResult CuisinesList()
        {
            return View();
        }

        /// <summary>
        /// Manage Menu Item
        /// </summary>
        /// <returns></returns>
        [Route(CommonConstants.RoutManageMenuItems)]
        public IActionResult MenuItems()
        {
            return View();
        }
    }
}
