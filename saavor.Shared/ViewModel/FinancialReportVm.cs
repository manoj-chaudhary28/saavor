using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace saavor.Shared.ViewModel
{
    public class FinancialReportVm
    {
        //public int OrderMonth { get; set; }
        //public int OrderYear { get; set; }
        public string KitchenId { get; set; }
        public string OrderDate { get; set; }
        public Int32 NumberOfOrders { get; set; }
        public string OrderAmount { get; set; }
        public string Discount { get; set; }
        public string SaavorDiscount { get; set; }
        public string SalesTax { get; set; }
        public string ServiceCharge { get; set; }
        public string DeliveryFee { get; set; }
        public string TipAmount { get; set; }
        [System.ComponentModel.DataAnnotations.Key]
        public string TotalAmount { get; set; }
        public string AmountToCustomer { get; set; }
        public string StripeFee { get; set; }
        public string SubTotal { get; set; }
    }

    public class FinancialMonthYear
    {
        [Key]
        public string OrderDate { get; set; }
    }

    public class FinancialVm
    {
        public List<FinancialMonthYear> MonthYearList { get; set; }
        public List<FinancialReportVm> Data { get; set; }
        public List<SelectListItem> Kitchens { get; set; }
        public string KitchenId { get; set; }
    }


    public class KitchenList
    {
        [Key]
        public Int64 ProfileId { get; set; }
        public Int64 KitchenId { get; set; }
        public string KitchenName { get; set; }
    }
}
