using System;
using System.ComponentModel.DataAnnotations;

namespace saavor.Shared.DTO.Notification
{
    /// <summary>
    /// NotificationDTO
    /// </summary>
    public class NotificationDTO
    {
        /// <summary>
        /// PendingOrders
        /// </summary>
        /// 
        [Key]
        public Int64 FoodOrderId { get; set; }
        /// <summary>
        /// OrderNumber
        /// </summary>
        public string OrderNumber { get; set; }
        /// <summary>
        /// KitchenName
        /// </summary>
        public string KitchenName { get; set; }
        /// <summary>
        /// UserName
        /// </summary>
        public string UserName { get; set; }
    }
}
