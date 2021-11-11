using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace saavor.Shared.ViewModel
{
    public class KitchenManageOrdersReportVm
    {
        [Key]
        public long Id { get; set; }
        public string OrderId { get; set; }
        public string OrderDate { get; set; }
        public string UserName { get; set; }

        public string OrderStatus { get; set; }
        public string DishOrders { get; set; }
        public string AddressLine { get; set; }
        public int TotalRecord { get; set; }
     
    }
    public class KitchenOrdersReportVm
    {
        public long Id { get; set; }
        public string OrderId { get; set; }
        public string OrderDate { get; set; }
        public string UserName { get; set; }
        public string OrderStatus { get; set; }
        public string DishOrders { get; set; }
        public string AddressLine { get; set; }
        public List<KitchenOrderDishesVm> OrderDishes { get; set; }
    }
}
