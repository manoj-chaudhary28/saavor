using System.ComponentModel.DataAnnotations;

namespace saavor.Shared.ViewModel
{
    public class KitchenUserInformationVm
    {
        [Key]
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string ProfileImagePath { get; set; }
        public string Status { get; set; }
        public string CreateDate { get; set; }
        public string Address { get; set; }
        public int TotalOrder { get; set; }
        public int CancelledOrder { get; set; }
        public int TotalReviews { get; set; }
        public int Tickets { get; set; }
    }
}
