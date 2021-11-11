using System;
using System.Collections.Generic;

#nullable disable

namespace saavor.EntityFrameworkCore.Entites
{
    public partial class ScCountry
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string ShortName { get; set; }
        public string DialCode { get; set; }
        public bool? CountryStatus { get; set; }
    }
}
