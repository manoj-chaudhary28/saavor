using System;
using System.Collections.Generic;

#nullable disable

namespace saavor.EntityFrameworkCore.Entites
{
    public partial class SkKitchenRequestStatus
    {
        public SkKitchenRequestStatus()
        {
            SkKitchenRequests = new HashSet<SkKitchenRequest>();
        }

        public int Id { get; set; }
        public string Status { get; set; }

        public virtual ICollection<SkKitchenRequest> SkKitchenRequests { get; set; }
    }
}
