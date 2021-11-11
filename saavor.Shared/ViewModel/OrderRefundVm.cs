using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace saavor.Shared.ViewModel
{
    public class OrderRefundVm
    {
        public string FoodOrderId { get; set; }
        public string Card { get; set; }
        public decimal OrderAmount { get; set; }
        public List<OrderItemModel> DishesItem { get; set; }
    }

    public class OrderItemVm
    {
        [Key]
        public Int32 DishId { get; set; }
        public string DishName { get; set; }
        public string Price { get; set; }
        public Int32? TotalQuantity { get; set; }
        public string Quantity { get; set; }
        public string Total { get; set; }
        public string Card { get; set; }
        public int FoodOrderId { get; set; }
        public Int64 FoodOrderDetailId { get; set; }
        public string LineItem { get; set; }     
        public Int32 AlreadyRefunded { get; set; }
    }

    public class RefundItemInput
    {
        public decimal RefundAmount { get; set; }
        public string RefundReason { get; set; }
        public string FoodOrderId { get; set; }
        public int ProfileId { get; set; }
        public string LineItems { get; set; }
        public string CurrentDate { get; set; }
       
    }

    public class ResponseDTO
    {
        public string ReturnCode { get; set; }
        public string ReturnMessage { get; set; }         
    }

    public class DishAddOns
    {
        [Key]
        public string ItemName { get; set; }
        public string ItemCost { get; set; }
    }
    public class OrderItemModel
    {
        [Key]
        public Int32 DishId { get; set; }
        public string DishName { get; set; }
        public string Price { get; set; }
        public Int32? TotalQuantity { get; set; }
        public string Quantity { get; set; }
        public string Total { get; set; }
        public string Card { get; set; }
        public int FoodOrderId { get; set; }
        public Int64 FoodOrderDetailId { get; set; }
        public string LineItem { get; set; }
        public List<DishAddOns> DishAddOns { get; set; }
        public Int32 AlreadyRefunded { get; set; }
    }
    public class RefundItemDetail
    {
        [Key]
        public string Price { get; set; }
        public string Quantity { get; set; }
        public string LineItem { get; set; }
        public string PricePerItem { get; set; }
       
    }
}
