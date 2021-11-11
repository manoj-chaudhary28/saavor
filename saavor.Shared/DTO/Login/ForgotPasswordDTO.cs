using Microsoft.AspNetCore.Http;
using saavor.Shared.ViewModel;
using System.ComponentModel.DataAnnotations;
namespace saavor.Shared.DTO.Login
{
    public class ForgotPasswordDTO
    {
        public string UserId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(5)]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [MinLength(5)]
        public string ConfirmPassword { get; set; }
        
        public string Message { get; set; } = string.Empty;
    }
}
