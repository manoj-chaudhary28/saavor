 using System.ComponentModel.DataAnnotations; 
namespace saavor.Shared.ViewModel
{
    public class KitchenDashboardKitchenVm
    {
        [Key]
        public int RowNumber { get; set; }
        public long ProfileId { get; set; }
        public long KitchenId { get; set; }
        public string KitchenName { get; set; }
        public string KitchenAddress { get; set; }
        public int TotalRecord { get; set; }
        public string ProtectedProfileId { get; set; }
    }
}
