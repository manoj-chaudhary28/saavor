using System.ComponentModel.DataAnnotations;

namespace saavor.Shared.ViewModel
{
    public class KitchenFoodOrderInvoiceDetailVm
    {
        [Key]
        public string OrderNumber { get; set; }
        public string KitchenName { get; set; }
        public string Address1 { get; set; }
        public string ZipCode { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public string PhoneNo { get; set; }
        public string DeliveryBy { get; set; }
        public string OrderDate { get; set; }
        public string DeliverySlot { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string UserAddress1 { get; set; }
        public string Zip { get; set; }
        public string UserStateName { get; set; }
        public string UserCityName { get; set; }
        public string CardType { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNumber { get; set; }
        public decimal SubTotal { get; set; }
        public decimal DealDiscount { get; set; }
        public decimal PromoCodeDiscount { get; set; }
        public decimal SalesTax { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal TipAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal GratuityAmount { get; set; }
        public string OrderNote { get; set; }
        public string DeliveryNote { get; set; }
        public decimal TaxesCharges { get; set; }
        public decimal PromoCodeAndDiscount { get; set; }
    }
}
