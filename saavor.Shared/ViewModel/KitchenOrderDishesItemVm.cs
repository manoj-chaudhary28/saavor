using System;
using System.ComponentModel.DataAnnotations;
namespace saavor.Shared.ViewModel
{
    public class KitchenOrderDishesItemVm
    {
        [Key]
        public Int32 DishId { get; set; }
        public string DishName { get; set; }
        public decimal Price { get; set; }
        public string Quantity { get; set; }
        public decimal Total { get; set; }
    }
}
