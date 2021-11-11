using System;
using System.Collections.Generic;
using System.Text;

namespace saavor.Shared.DTO.ManageUsers
{
    public class ManageUserInputDTO
    {
        public Int64 ProfileId { get; set; }
        public Int64 UserId { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public string Search { get; set; }
    }
}
