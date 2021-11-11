using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace saavor.Shared.ViewModel
{
    /// <summary>
    /// UserVm
    /// </summary>
    public class UserVm
    {
        [Key]
        public Int64 UserId { get; set; }
        /// <summary>
        /// FirstName
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// LastName
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// BusinessName
        /// </summary>
        public string BusinessName { get; set; }
        /// <summary>
        /// Contact
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// CountryId
        /// </summary>
        public Int32 CountryId { get; set; }
        /// <summary>
        /// StateId
        /// </summary>
        public Int32 StateId { get; set; }
        /// <summary>
        /// City
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Address
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// TaxId
        /// </summary>
        public string TaxId { get; set; }
        /// <summary>
        /// BusinessLogo
        /// </summary>
        public string BusinessLogo { get; set; }
        /// <summary>
        /// CountryName
        /// </summary>
        public string CountryName { get; set; }
        /// <summary>
        /// StateName
        /// </summary>
        public string StateName { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
    }
}
