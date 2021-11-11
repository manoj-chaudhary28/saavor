using System.ComponentModel.DataAnnotations;
namespace saavor.Shared.DTO.Login
{
    public class LoginInputDTO
    {
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email address is required")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Message { get; set; }
    }
}
