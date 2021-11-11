
using System.ComponentModel.DataAnnotations;

namespace saavor.Shared.ViewModel
{
    public class KitchenCountVm
    {
        [Key]
        public string TotalMenus { get; set; }
        public string TotalRevenue { get; set; }
        public string TotalOrder { get; set; }
        public string TotalCustomers { get; set; }
    }
}
