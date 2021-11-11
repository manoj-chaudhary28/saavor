using System;
using System.Collections.Generic;

#nullable disable

namespace saavor.EntityFrameworkCore.Entites
{
    public partial class SkKitchenRequest
    {
        public long Id { get; set; }
        public int? KitchenProfileId { get; set; }
        public long? UserId { get; set; }
        public int? Status { get; set; }
        public string Reason { get; set; }
        public DateTime? CreateDate { get; set; }

        public virtual SkKitchen KitchenProfile { get; set; }
        public virtual SkKitchenRequestStatus StatusNavigation { get; set; }
    }
}
