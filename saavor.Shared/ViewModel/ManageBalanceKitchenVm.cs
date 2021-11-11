using System.ComponentModel.DataAnnotations;

namespace saavor.Shared.ViewModel
{
    /// <summary>
    /// ManageBalanceKitchenVm
    /// </summary>
    public class ManageBalanceKitchenVm
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
        /// FormatedDate
        /// </summary>
        public string FormatedDate { get; set; }
    }
}
