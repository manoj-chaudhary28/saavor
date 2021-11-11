using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using saavor.Shared.Constants;
using saavor.Web.Models;

namespace saavor.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {           
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("GET Pages.PrivacyModel called.");
            return View();
        }

        [HttpPost]
        public JsonResult GetBrowserDate(string browserDate,string formate)
        {
            try
            {
                string date = Convert.ToDateTime(browserDate).ToString(formate);
                string time = Convert.ToDateTime(browserDate).ToString("hh:mm tt");
                return Json(date + "-" + time);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Json(DateTime.Now.ToString("ddd MMM yy hh:mm tt"));
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route(CommonConstants.RoutError)]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {

            return View();
        }
    }
}
