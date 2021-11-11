using System;
using System.Collections.Generic;

#nullable disable

namespace saavor.EntityFrameworkCore.Entites
{
    public partial class ScState
    {
        public int StateId { get; set; }
        public int CountryId { get; set; }
        public string StateName { get; set; }
        public string ShortName { get; set; }
        public bool? StateStatus { get; set; }
        public bool? IsCategory { get; set; }
        public string CategDesc { get; set; }
        public string CategUrl { get; set; }
        public DateTime? CategPubDate { get; set; }
        public DateTime? CategCreateDate { get; set; }
        public DateTime? CategModifyDate { get; set; }
    }
}
