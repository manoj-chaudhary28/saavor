using System;
using System.ComponentModel.DataAnnotations;

namespace saavor.Shared.ViewModel
{
    public class Response
    {
        [Key]
        public Int32 Code { get; set; }
        public Boolean Status { get; set; }

    }
}
