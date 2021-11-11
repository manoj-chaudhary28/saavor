
using System.ComponentModel.DataAnnotations;

namespace saavor.Shared.ViewModel
{
    public class CommonVm
    {
        [Key]
        public string Id { get; set; }
        public string Status { get; set; }
    }
}
