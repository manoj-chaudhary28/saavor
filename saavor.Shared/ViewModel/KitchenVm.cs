using saavor.Shared.Filter;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace saavor.Shared.ViewModel
{
    public class KitchenVm
    {
        public List<KitchenDTO> Kitchen { get; set; }
    }

    public class KitchenDTO
    {
        [Key]
        public long ProfileId { get; set; }
        public long KitchenId { get; set; }
        public string KitchenName { get; set; }
        public string KitchenAddress { get; set; }
        public long? RequestId { get; set; }
        public int TotalRecord { get; set; }
    }
}
