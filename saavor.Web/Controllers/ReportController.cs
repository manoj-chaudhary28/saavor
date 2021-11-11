using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using saavor.Application.BalanceKitchen.Query;
using saavor.Application.Kitchen.Query;
using saavor.Shared.AppSettings;
using saavor.Shared.Constants;
using saavor.Shared.DTO.Kitchen;
using saavor.Shared.Filter;
using saavor.Shared.Interfaces;
using saavor.Shared.SearchModel;
using saavor.Shared.ViewModel;

namespace saavor.Web.Controllers
{
    /// <summary>
    /// ReportController
    /// </summary>
    [Authorize]    
    public class ReportController : Controller
    {
        /// <summary>
        /// environment
        /// </summary>
        private readonly IWebHostEnvironment environment;
        /// <summary>
        /// claimService
        /// </summary>
        private readonly IClaimService claimService;
        /// <summary>
        /// getKitchenBalanceQuery
        /// </summary>
        private readonly GetKitchenBalanceQuery getKitchenBalanceQuery;
        /// <summary>
        /// getKitchenDashboardQuery
        /// </summary>
        private readonly GetKitchenDashboardQuery getKitchenDashboardQuery;
        /// <summary>
        /// protector
        /// </summary>
        private readonly IDataProtector protector;
        /// <summary>
        /// dataProtectionAppSettings
        /// </summary>
        private readonly DataProtection dataProtectionAppSettings;
        /// <summary>
        /// PageSizeAppSettings
        /// </summary>
        private readonly PageSize pageSizeAppSettings;
        /// <summary>
        /// appUsers
        /// </summary>
        private readonly AppUsers appUsers;
        /// <summary>
        /// httpContextAccessor
        /// </summary>
        private readonly IHttpContextAccessor httpContextAccessor;
        /// <summary>
        /// clientFactory
        /// </summary>
        private readonly IHttpClientFactory clientFactory;
        /// <summary>
        /// client
        /// </summary>
        private readonly HttpClient client;
        /// <summary>
        /// logger
        /// </summary>
        private readonly ILogger<ReportController> logger;

        /// <summary>
        /// ReportController
        /// </summary>
        /// <param name="iClaimServiceInstance"></param>
        /// <param name="getKitchenDashboardQueryInstance"></param>
        /// <param name="dataProtectionAppSettingsInstacne"></param>
        /// <param name="providerInstance"></param>
        /// <param name="httpContextAccessor"></param>
        /// <param name="pageSizeAppSettingsInstacne"></param>
        /// <param name="appUsersInstance"></param>
        /// <param name="envInitiator"></param>
        /// <param name="clientFactory"></param>
        /// <param name="client"></param>
        /// <param name="logger"></param>
        /// <param name="getKitchenBalanceQueryInstance"></param>
        public ReportController(IClaimService iClaimServiceInstance
                                          , GetKitchenDashboardQuery getKitchenDashboardQueryInstance
                                          , IOptions<DataProtection> dataProtectionAppSettingsInstacne
                                          , IDataProtectionProvider providerInstance
                                          , IHttpContextAccessor httpContextAccessorInstance
                                          , IOptions<PageSize> pageSizeAppSettingsInstacne
                                          , IOptions<AppUsers> appUsersInstance
                                          , IWebHostEnvironment envInitiator
                                          , IHttpClientFactory clientFactoryInstacne
                                          , HttpClient clientInstacne
                                          , ILogger<ReportController> loggerInstacne
                                          , GetKitchenBalanceQuery getKitchenBalanceQueryInstance)
        {
            claimService = iClaimServiceInstance;
            getKitchenDashboardQuery = getKitchenDashboardQueryInstance;
            dataProtectionAppSettings = dataProtectionAppSettingsInstacne.Value;
            protector = providerInstance.CreateProtector(dataProtectionAppSettings.Key);
            httpContextAccessor = httpContextAccessorInstance;
            pageSizeAppSettings = pageSizeAppSettingsInstacne.Value;
            appUsers = appUsersInstance.Value;
            environment = envInitiator;
            clientFactory = clientFactoryInstacne;
            client = clientInstacne;
            logger = loggerInstacne;
            getKitchenBalanceQuery = getKitchenBalanceQueryInstance;
        }

        /// <summary>
        /// Order
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public IActionResult Order(int pageNumber, string fromDate, string toDate)
        {
            try
            {
                int totalRecord = 0;
                pageNumber = pageNumber == 0 ? 1 : pageNumber;

                if (!string.IsNullOrEmpty(fromDate))
                    TempData["startDate"] = fromDate;
                else
                {
                    fromDate = DateTime.Now.Date.ToString("MM-dd-yyyy");
                    TempData["startDate"] = fromDate;
                }
                if (!string.IsNullOrEmpty(toDate))
                    TempData["endDate"] = toDate;
                else
                {
                    toDate = DateTime.Now.Date.ToString("MM-dd-yyyy");
                    TempData["endDate"] = toDate;
                }
                TempData["SelectedFilter"] = "From " + fromDate + " to " + toDate;
                var input = new KitchenInputDTO()
                {
                    UserId = Convert.ToInt64(claimService.GetClaim(CommonConstants.SaavorUserId)),
                    Page = pageNumber,
                    Size = Convert.ToInt32(pageSizeAppSettings.Size),
                    FromDate = fromDate,
                    ToDate = toDate,
                    ProfileId = Convert.ToInt64(claimService.GetClaim(CommonConstants.ProfileId)),
                    //ProfileId = 1478,
                    OrderType = "Pending,Preparation"
                };

                List<KitchenManageOrdersReportVm> orderReportList = new List<KitchenManageOrdersReportVm>();
                orderReportList = getKitchenDashboardQuery.KitchenManageOrdersReport(input).Result;

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
                ViewBag.Paging = SetPaging.Set_Paging_Dates(pageNumber, Convert.ToInt32(pageSizeAppSettings.Size), totalRecord, "activeLink", Url.Action("order", "report"), "disableLink", fromDate, toDate);
                return View(_list);

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return RedirectToAction("error", "home");
            }
        }

        /// <summary>
        /// GetExportOrders
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetExportOrders(string fromDate, string toDate)
        {
            try
            {
                var input = new KitchenInputDTO()
                {
                    UserId = Convert.ToInt64(claimService.GetClaim(CommonConstants.SaavorUserId)),
                    ProfileId = Convert.ToInt64(claimService.GetClaim(CommonConstants.ProfileId)),
                    FromDate=fromDate,
                    ToDate = toDate
                };

                List<KitchenManageOrdersReportVm> orderReportList = new List<KitchenManageOrdersReportVm>();
                orderReportList = getKitchenDashboardQuery.KitchenManageOrdersReportToExport(input).Result;

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
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return PartialView("_OrdersReport", new List<KitchenOrdersReportVm>());

            }
        }


        /// <summary>
        /// Financial
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public IActionResult Financial(int pageNumber, string fromDate, string toDate)
        {
            try
            {
                List<SelectListItem> kitchenSelectListItem = new List<SelectListItem>();
                var inputDTO = LoadInput(pageNumber, fromDate, toDate, string.Empty);
                logger.LogError(inputDTO.FromDate + " ~" + inputDTO.ToDate);

                var kitchenList = getKitchenBalanceQuery.GetKitchens(inputDTO).Result;

                if( kitchenList !=null && kitchenList.Count > 0)
                {
                   kitchenSelectListItem = kitchenList.Select(x => new SelectListItem { Text = x.KitchenId + " - " + x.KitchenName, Value = x.ProfileId.ToString() }).ToList();
                }

                var model = new FinancialVm()
                {
                    Kitchens = kitchenSelectListItem
                };
                return View(model);

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return RedirectToAction("error", "home");
            }
        }

        /// <summary>
        /// GetFinancialReport
        /// </summary>
        /// <param name="financialSearchDTO"></param>
        /// <returns></returns>
        
        [HttpPost]
        public ActionResult GetFinancialReport([FromBody] FinancialSearchDTO  financialSearchDTO)
        {
            try
            {
                var inputDTO = LoadInput(1, financialSearchDTO.StartDate, financialSearchDTO.Enddate,financialSearchDTO.ProfileIds);
                var monthYearList = getKitchenBalanceQuery.GetFinancialMonthYear(inputDTO).Result;
                var model = new FinancialVm()
                {
                    MonthYearList = monthYearList,
                    Data = LoadList(getKitchenBalanceQuery.GetFinancialReport(inputDTO).Result)
                };
                return PartialView("_FinancialReport", model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return PartialView("_FinancialReport", new FinancialVm());
            }
        }

        /// <summary>
        /// Balance
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public IActionResult Balance(int pageNumber, string fromDate, string toDate)
        {
            try
            {
                var inputDTO = LoadInput(pageNumber, fromDate, toDate, string.Empty);
                 
                List<ManageBalanceKitchenVm> manageBalanceKitchenVms = new List<ManageBalanceKitchenVm>();
                manageBalanceKitchenVms = getKitchenBalanceQuery.GetManagebalance(inputDTO).Result;

                List<ManageBalanceKitchenVm> list = new List<ManageBalanceKitchenVm>();
                if (manageBalanceKitchenVms != null && manageBalanceKitchenVms.Count > 1)
                {
                    var res = manageBalanceKitchenVms.Select(x => new
                    {
                        x.TotalAmount
                    }).FirstOrDefault();

                    if (!string.IsNullOrEmpty(Convert.ToString(res.TotalAmount)))
                    {
                        list = manageBalanceKitchenVms.Select(item => new ManageBalanceKitchenVm
                        {
                            FormatedDate = item.FormatedDate,
                            CreateDate = item.CreateDate,
                            Amount = item.Amount,
                            ServiceCharge = item.ServiceCharge,                             
                            TotalAmount = item.TotalAmount,
                            TotalTransaction = item.TotalTransaction
                        }).ToList();
                    }


                }
                //ViewBag.Paging = SetPaging.Set_Paging(pageNumber, Convert.ToInt32(_PageSizeAppSettings.Size), totalRecord, "activeLink", Url.Action("managereports", "kitchendashboard"), "disableLink", search);
                return View(list);

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return RedirectToAction("error", "home");
            }
        }
               
        /// <summary>
        /// GetBalanceInvoice
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>

        [HttpPost]
        public ActionResult GetBalanceInvoice(string search)
        {
            try
            {
                var input = new KitchenInputDTO()
                {
                    UserId = Convert.ToInt64(claimService.GetClaim(CommonConstants.SaavorUserId)),
                    ProfileId = Convert.ToInt64(claimService.GetClaim(CommonConstants.ProfileId)),
                    Search = search,
                };

                ManageBalanceKitchenInvoiceVm balanceInvoice = new ManageBalanceKitchenInvoiceVm();
                balanceInvoice = getKitchenBalanceQuery.GetManagebalanceInvoice(input).Result;
                
                return PartialView("_BalanceInvoice", balanceInvoice);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return PartialView("_BalanceInvoice", new ManageBalanceKitchenInvoiceVm());

            }
        }
               
        /// <summary>
        /// ReturnMonthYear
        /// </summary>
        /// <param name="date"></param>
        /// <param name="isMonth"></param>
        /// <returns></returns>
        private static int ReturnMonthYear(string date, bool isMonth)
        {
            if (!string.IsNullOrEmpty(date))
            {
                string[] array = date.Split("-");
                if (array.Length > 0)
                {
                    return Convert.ToInt32(array[isMonth ? 0 : 1]);
                }
            }
            return Convert.ToInt32(isMonth ? DateTime.Now.Month : DateTime.Now.Year);
        }

        /// <summary>
        /// ReturnDate
        /// </summary>
        /// <param name="monthDays"></param>
        /// <param name="monthYear"></param>
        /// <returns></returns>
        private static string ReturnDate(int monthDays, string monthYear)
        {
            string[] array = monthYear.Split("-");
            string date = string.Empty;
            if (array.Length > 0)
            {
                date = array[1] + "-" + array[0] + "-" + Convert.ToString(monthDays);

            }
            return date;
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
        /// LoadInput
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        private KitchenInputDTO LoadInput(int pageNumber, string fromDate, string toDate, string profileIds)
        {
            pageNumber = pageNumber == 0 ? 1 : pageNumber;

            if (!string.IsNullOrEmpty(fromDate))
                TempData["FromDate"] = fromDate;
            else
            {
                fromDate = DateTime.Now.Date.ToString("MM-dd-yyyy");
                TempData["FromDate"] = fromDate;
            }

            if (!string.IsNullOrEmpty(toDate))
                TempData["ToDate"] = toDate;
            else
            {
                toDate = DateTime.Now.Date.ToString("MM-dd-yyyy");
                TempData["ToDate"] = toDate;
            }
            TempData["SelectedFilter"] = "From " + fromDate + " to " + toDate;
            int currentFromMontday = DateTime.DaysInMonth(ReturnMonthYear(fromDate, false), ReturnMonthYear(fromDate, true));
            int currentToMontday = DateTime.DaysInMonth(ReturnMonthYear(toDate, false), ReturnMonthYear(toDate, true));
           return new KitchenInputDTO()
            {
                UserId = Convert.ToInt64(claimService.GetClaim(CommonConstants.SaavorUserId)),
                Page = pageNumber,
                Size = Convert.ToInt32(pageSizeAppSettings.Size),
                ProfileId = Convert.ToInt64(claimService.GetClaim(CommonConstants.ProfileId)),
               //FromDate = ReturnDate(currentFromMontday, fromDate),
               //ToDate = ReturnDate(currentToMontday, toDate),
               FromDate =  fromDate,
               ToDate =  toDate,
               MultiIds = profileIds
           };
        }

        /// <summary>
        /// LoadList
        /// </summary>
        /// <param name="financialReport"></param>
        /// <returns></returns>
        private List<FinancialReportVm> LoadList(List<FinancialReportVm> financialReport)
        {
            List<FinancialReportVm> list = new List<FinancialReportVm>();
            if (financialReport != null && financialReport.Count > 0)
            {
                var res = financialReport.Select(x => new
                {
                    x.TotalAmount
                }).FirstOrDefault();

                if (!string.IsNullOrEmpty(res.TotalAmount))
                {
                    var i = 1;
                    foreach (var item in financialReport)
                    {
                        list.Add(
                            new FinancialReportVm
                            {
                                KitchenId = item.KitchenId,
                                NumberOfOrders = item.NumberOfOrders,
                                OrderDate = GetTotalRow(item.OrderDate, i, financialReport.Count),
                                OrderAmount = item.OrderAmount,
                                Discount = item.Discount,
                                SaavorDiscount = item.SaavorDiscount,
                                AmountToCustomer = item.AmountToCustomer,
                                SalesTax = item.SalesTax,
                                ServiceCharge = item.ServiceCharge,
                                DeliveryFee = item.DeliveryFee,
                                TipAmount = item.TipAmount,
                                TotalAmount = item.TotalAmount,
                                StripeFee = item.StripeFee,
                                SubTotal = item.SubTotal
                            }
                        );
                        i++;
                    }

                    //list = financialReport.Select(item => new FinancialReportVm
                    //{
                    //    KitchenId = item.KitchenId,
                    //    OrderDate = item.OrderDate,
                    //    OrderAmount = item.OrderAmount,
                    //    Discount = item.Discount,
                    //    SalesTax = item.SalesTax,
                    //    ServiceCharge = item.ServiceCharge,
                    //    DeliveryFee = item.DeliveryFee,
                    //    TipAmount = item.TipAmount,
                    //    TotalAmount = item.TotalAmount,
                    //}).ToList();
                }
            }
            return list;
        }

        /// <summary>
        /// GetTotalRow
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="index"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        private string GetTotalRow(string orderDate,int index,int totalRecords)
        {
            if (totalRecords > 2)
                return orderDate;
            else
            {
                if (index == totalRecords)
                    return string.Empty;
                else
                    return orderDate;
            }
        }

    }
}
