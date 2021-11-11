using System.ComponentModel.DataAnnotations;
namespace saavor.Shared.ViewModel
{
    public class LoginVm
    {
        [Key]
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BusinessName { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public string Password { get; set; }
        public string LoginType { get; set; }
        public string ProfileId { get; set; }
        public string KitchenName { get; set; }
        public int IsAprovedKitchenRequest { get; set; }
    }
}
