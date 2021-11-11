

using System.ComponentModel.DataAnnotations;

namespace saavor.Shared.ViewModel
{
    public class OrderCountVm
    {
        [Key]
        public int New { get; set; }
        public int Ready { get; set; }
        public int Delivered { get; set; }
        public int HerePickup { get; set; }
        public int Delivery { get; set; }
    }
}
