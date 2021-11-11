using System;

namespace saavor.Shared.DTO.Kitchen
{
    public class KitchenInputDTO
    {
        public Int64 KitchenId { get; set; }
        public Int64 ProfileId { get; set; }
        public Int64 UserId { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public string Search { get; set; }
        public string OrderType { get; set; }
        public string Type { get; set; } = string.Empty;
        public int IsDeliver { get; set; } = 0;
        public Int64 ReviewId { get; set; }
        public string FromDate { get; set; } = string.Empty;
        public string ToDate { get; set; } = string.Empty;
        public string MultiIds { get; set; } = string.Empty;
    }
}

