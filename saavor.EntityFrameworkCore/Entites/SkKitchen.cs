using System;
using System.Collections.Generic;

#nullable disable

namespace saavor.EntityFrameworkCore.Entites
{
    public partial class SkKitchen
    {
        public SkKitchen()
        {
            SkKitchenRequests = new HashSet<SkKitchenRequest>();
        }

        public long KitchenId { get; set; }
        public int ApplicationId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BusinessType { get; set; }
        public string BusinessName { get; set; }
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }
        public string FaxNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CountryNameOld { get; set; }
        public string StateNameOld { get; set; }
        public string CityNameOld { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Zipcode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string ProfileImagePath { get; set; }
        public string TaxId { get; set; }
        public string CompanyLegalName { get; set; }
        public string TaxIdnumber { get; set; }
        public string Ssn { get; set; }
        public string SsnlegalName { get; set; }
        public string Ssnumber { get; set; }
        public bool? IsDelivery { get; set; }
        public decimal? DeliveryRadius { get; set; }
        public bool? IsKitchenDelivery { get; set; }
        public bool? IsVerify { get; set; }
        public string Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string Mi { get; set; }
        public int? TempKitchenId { get; set; }
        public int ProfileId { get; set; }
        public long? SessionToken { get; set; }
        public string KitchenName { get; set; }
        public string DlfilePath { get; set; }
        public string StateIdfilePath { get; set; }
        public string W9filePath { get; set; }
        public DateTime? Dob { get; set; }
        public string Languages { get; set; }
        public string StateCode { get; set; }
        public decimal? MinOrderAmount { get; set; }
        public int? AvgDeliveryTime { get; set; }
        public decimal? DeliveryCharges { get; set; }
        public decimal? FreeDeliveryLimitAmount { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public int? CityId { get; set; }
        public string UserImagePath { get; set; }
        public int? AccStatus { get; set; }
        public string AccStatusReason { get; set; }
        public int? ParentId { get; set; }
        public string Gender { get; set; }
        public string UserTitle { get; set; }
        public string CustomerId { get; set; }
        public int? IsFullApplicationSubmitted { get; set; }
        public string Locality { get; set; }
        public bool? Isdelete { get; set; }
        public string RejectRemarke { get; set; }
        public DateTime? RejectDate { get; set; }
        public bool? SrvRejectMail { get; set; }
        public DateTime? SrvRejectMailDate { get; set; }
        public decimal? SaleTax { get; set; }
        public decimal? SalesTaxPer { get; set; }
        public bool? RejectMail { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public bool? IsCheckrPayment { get; set; }
        public string DeviceId { get; set; }
        public string FullName { get; set; }
        public string MacAddress { get; set; }
        public string IpAddress { get; set; }
        public string KitchenMessage { get; set; }
        public int? TimeToAcceptOrder { get; set; }
        public int? KitchenVisibilityRange { get; set; }
        public bool? DirectToCustomer { get; set; }
        public bool? IsPickUp { get; set; }
        public bool? DispalyCalories { get; set; }
        public bool? BusinessToBusiness { get; set; }

        public virtual ICollection<SkKitchenRequest> SkKitchenRequests { get; set; }
    }
}
