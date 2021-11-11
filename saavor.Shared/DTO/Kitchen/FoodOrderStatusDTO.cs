using System;
 
namespace saavor.Shared.DTO.Kitchen
{
    /// <summary>
    /// FoodOrderStatusDTO
    /// </summary>
    public class FoodOrderStatusDTO
    {
        /// <summary>
        /// ProfileId
        /// </summary>
        public Int64 ProfileId { get; set; }
        /// <summary>
        /// FoodOrderId
        /// </summary>
        public string FoodOrderIds { get; set; }
        /// <summary>
        /// OrderStatus
        /// </summary>
        public string OrderStatus { get; set; }
        /// <summary>
        /// SystemDateTime
        /// </summary>
        public string SystemDateTime { get; set; }
        /// <summary>
        /// RejectReason
        /// </summary>
        public string RejectReason { get; set; }
    }

    public class OrderStatusDTO
    {
        /// <summary>
        /// ProfileId
        /// </summary>
        public Int64 ProfileId { get; set; }
        /// <summary>
        /// OrderId
        /// </summary>
        public Int64 OrderId { get; set; }
        /// <summary>
        /// OrderStatus
        /// </summary>
        public string OrderStatus { get; set; }
        /// <summary>
        /// CurrentDate
        /// </summary>
        public string CurrentDate { get; set; }
       
    }

    public class OrderStatusAcceptRejectDTO
    {
        /// <summary>
        /// ProfileId
        /// </summary>
        public Int64 ProfileId { get; set; }
        /// <summary>
        /// FoodOrderId
        /// </summary>
        public Int64 OrderId { get; set; }
        /// <summary>
        /// OrderStatus
        /// </summary>
        public string OrderStatus { get; set; }
        /// <summary>
        /// SystemDateTime
        /// </summary>
        public string CurrentDate { get; set; }
        /// <summary>
        /// RejectReason
        /// </summary>
        public string RejectReason { get; set; }
        /// <summary>
        /// DeliveryBy 
        /// </summary>
        public string DeliveryBy { get; set; }
    }
}
