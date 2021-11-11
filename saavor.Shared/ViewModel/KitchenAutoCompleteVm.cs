
using System.ComponentModel.DataAnnotations;

namespace saavor.Shared.ViewModel
{
    public class KitchenAutoCompleteVm
    {
        [Key]
        public long KitchenId { get; set; }
        public string KitchenName { get; set; }

    }
}
