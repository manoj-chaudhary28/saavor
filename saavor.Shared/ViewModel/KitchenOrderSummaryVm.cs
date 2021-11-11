
using System.ComponentModel.DataAnnotations;

namespace saavor.Shared.ViewModel
{
    public class KitchenOrderSummaryVm
    {
        public decimal OrderPercentage { get; set; }
        public int Delivered { get; set; }
        public int Cancelled { get; set; }
        public int Rejected { get; set; }
        [Key]
        public int TotalOrder { get; set; }
        public int Pickup { get; set; }
    }
}
