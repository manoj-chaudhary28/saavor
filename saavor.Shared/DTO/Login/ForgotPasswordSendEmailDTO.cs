using System.ComponentModel.DataAnnotations;
namespace saavor.Shared.DTO.Login
{
    public class ForgotPasswordSendEmailDTO
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email address is required")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
