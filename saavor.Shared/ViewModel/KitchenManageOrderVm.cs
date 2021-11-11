using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace saavor.Shared.ViewModel
{
    public class KitchenManageOrderVm
    {
        [Key]
        public long Id { get; set; }
        public string OrderId { get; set; }
        public string OrderDate { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string OrderStatus { get; set; }
        public int TotalRecord { get; set; }
        public string FoodOrderId { get; set; }
        public int IsDelivery { get; set; }
        public string DishOrders { get; set; }
        public string RejectReason { get; set; }

    }

    public class KitchenOrdersVm
    {
        public long Id { get; set; }
        public string OrderId { get; set; }
        public string OrderDate { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string OrderStatus { get; set; }
        public int TotalRecord { get; set; }
        public string FoodOrderId { get; set; }
        public int IsDelivery { get; set; }
        public List<KitchenOrderDishesVm> OrderDishes { get; set; }
        public string RejectReason { get; set; }
    }
}
