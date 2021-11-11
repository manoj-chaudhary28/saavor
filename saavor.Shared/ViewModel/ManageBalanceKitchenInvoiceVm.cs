 using System.ComponentModel.DataAnnotations;

namespace saavor.Shared.ViewModel
{
    /// <summary>
    /// ManageBalanceKitchenInvoiceVm
    /// </summary>
    public class ManageBalanceKitchenInvoiceVm
    {
        [Key]
        public string CreateDate { get; set; }
        /// <summary>
        /// Amount
        /// </summary>
        public string Amount { get; set; }
        /// <summary>
        /// TotalTransaction
        /// </summary>
        public string TotalTransaction { get; set; }
        /// <summary>
        /// ServiceCharge
        /// </summary>
        public string ServiceCharge { get; set; }
        /// <summary>
        /// TotalAmount
        /// </summary>
        public string TotalAmount { get; set; }
        /// <summary>
        /// KitchenName
        /// </summary>
        public string KitchenName { get; set; }
        /// <summary>
        /// KitchenAddress
        /// </summary>
        public string KitchenAddress { get; set; }
        /// <summary>
        /// KitchenPhoneNo
        /// </summary>
        public string KitchenPhoneNo { get; set; }
        /// <summary>
        /// SalesTax
        /// </summary>
        public string SalesTax { get; set; }
        /// <summary>
        /// KitchenStateName
        /// </summary>
        public string KitchenStateName { get; set; }
        /// <summary>
        /// KitchenCityName
        /// </summary>
        public string KitchenCityName { get; set; }
        /// <summary>
        /// StartMonth
        /// </summary>
        public string StartMonth { get; set; }
        /// <summary>
        /// EndMonth
        /// </summary>
        public string EndMonth { get; set; }
         
    }
}
