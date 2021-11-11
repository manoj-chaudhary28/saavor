using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace saavor.Shared.ViewModel
{
   public class KitchenOrderInvoiceVm
    {
        public KitchenFoodOrderInvoiceDetailVm FoodOrderDetail { get; set; }
        public List<KitchenOrderDishesItemVm> DishesItem { get; set; }

    }
}
