
using System.ComponentModel.DataAnnotations;

namespace saavor.Shared.ViewModel
{
    public class KitchenChartVm
    {
        [Key]
        public string key { get; set; }
        public string value { get; set; }
    }
}
