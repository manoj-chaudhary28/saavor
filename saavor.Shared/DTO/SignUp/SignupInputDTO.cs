using Microsoft.AspNetCore.Http;
using saavor.Shared.ViewModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace saavor.Shared.DTO.SignUp
{
   public  class SignupInputDTO
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "First name is required")]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Last name is required")]
        [MaxLength(30)]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Business name is required")]
        [MaxLength(100)]
        public string BusinessName { get; set; }

        public string ContactCountryCode { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Contact is required")]
        [MaxLength(14)]
        public string Contact { get; set; }

       // [Required(AllowEmptyStrings = false, ErrorMessage = "Country is required")]
        [Required, Range(1, int.MaxValue, ErrorMessage = "Country is required")]
        public int Country { get; set; }

        // [Required(AllowEmptyStrings = false, ErrorMessage = "State is required")]
        [Required, Range(1, int.MaxValue, ErrorMessage = "State is required")]
        public int State { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "City is required")]
        [MaxLength(100)]
        public string City { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Address is required")]
        [MaxLength(100)]
        public string Address { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "TaxId is required")]
        [MaxLength(20)]
        public string TaxId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email address is required")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(5)]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [MinLength(5)]
        public string ConfirmPassword { get; set; }
        public IFormFile BusinessLogo { get; set; }
        public CountryStateVm CountryStateList { get; set; }
        public string FileName { get; set; }
        public string Message { get; set; } = string.Empty;
        public Int64 UserId { get; set; } = 0;
    }

    public class SignupUpdateInputDTO
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "First name is required")]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Last name is required")]
        [MaxLength(30)]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Business name is required")]
        [MaxLength(100)]
        public string BusinessName { get; set; }

        public string ContactCountryCode { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Contact is required")]
        [MaxLength(14)]
        public string Contact { get; set; }

        // [Required(AllowEmptyStrings = false, ErrorMessage = "Country is required")]
        [Required, Range(1, int.MaxValue, ErrorMessage = "Country is required")]
        public int Country { get; set; }

        // [Required(AllowEmptyStrings = false, ErrorMessage = "State is required")]
        [Required, Range(1, int.MaxValue, ErrorMessage = "State is required")]
        public int State { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "City is required")]
        [MaxLength(100)]
        public string City { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Address is required")]
        [MaxLength(100)]
        public string Address { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "TaxId is required")]
        [MaxLength(20)]
        public string TaxId { get; set; }

        public IFormFile BusinessLogo { get; set; }
        public CountryStateVm CountryStateList { get; set; }
        public string FileName { get; set; }
        public string Message { get; set; } = string.Empty;
        public Int64 UserId { get; set; } = 0;
        public string Email { get; set; } = string.Empty;
    }
}
