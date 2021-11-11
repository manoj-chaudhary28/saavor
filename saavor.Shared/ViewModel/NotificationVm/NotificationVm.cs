using saavor.Shared.DTO.Notification;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; 

namespace saavor.Shared.ViewModel.NotificationVm
{
    /// <summary>
    /// NotificationVm
    /// </summary>
    public class NotificationVm
    {
        /// <summary>
        /// PendingOrders
        /// </summary>
        /// 
        [Key]
        public Int32 Count { get; set; }
        /// <summary>
        /// Orders
        /// </summary>
        public List<NotificationDTO> Orders { get; set; }
    }
}
