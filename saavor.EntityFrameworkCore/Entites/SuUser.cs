using System;
using System.Collections.Generic;

#nullable disable

namespace saavor.EntityFrameworkCore.Entites
{
    public partial class SuUser
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BusinessName { get; set; }
        public int? CountryCodeId { get; set; }
        public string Contact { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string TaxId { get; set; }
        public string EmailAddress { get; set; }
        public string BusinessLogo { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
