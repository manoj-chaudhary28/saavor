
using System.ComponentModel.DataAnnotations;

namespace saavor.Shared.ViewModel
{
    public class KitchenUsersVm
    {
        [Key]
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string Status { get; set; }
        public string CreateDate { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public int TotalRecord { get; set; }

    }
}
