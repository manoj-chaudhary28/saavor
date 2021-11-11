using saavor.Shared.Enumrations;
using System;
 
namespace saavor.Shared.DTO.Kitchen
{
    public class KitchenRequestDTO
    {
        public int ProfileId { get; set; }
        public Int64 UserId { get; set; }
        public KitchenRequestStatusEnum Status { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
