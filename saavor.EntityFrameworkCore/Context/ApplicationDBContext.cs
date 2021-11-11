using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using saavor.EntityFrameworkCore.Entites;
using saavor.Shared.DTO.Notification;
using saavor.Shared.ViewModel;
using saavor.Shared.ViewModel.NotificationVm;

#nullable disable

namespace saavor.EntityFrameworkCore.Context
{
    public partial class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext()
        {
        }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }
        /// <summary>
        /// ScCountries
        /// </summary>
        public virtual DbSet<ScCountry> ScCountries { get; set; }
        /// <summary>
        /// ScStates
        /// </summary>
        public virtual DbSet<ScState> ScStates { get; set; }
        /// <summary>
        /// SkKitchens
        /// </summary>
        public virtual DbSet<SkKitchen> SkKitchens { get; set; }
        /// <summary>
        /// SkKitchenRequests
        /// </summary>
        public virtual DbSet<SkKitchenRequest> SkKitchenRequests { get; set; }
        /// <summary>
        /// SkKitchenRequestStatuses
        /// </summary>
        public virtual DbSet<SkKitchenRequestStatus> SkKitchenRequestStatuses { get; set; }
        /// <summary>
        /// SuUsers
        /// </summary>
        public virtual DbSet<SuUser> SuUsers { get; set; }
        /* Store-procedures view models for fetch the data */
        /// <summary>
        /// LoginRm
        /// </summary>
        public virtual DbSet<LoginVm> LoginRm { get; set; }
        /// <summary>
        /// CommonVm
        /// </summary>
        public virtual DbSet<CommonVm> CommonVm { get; set; }
        //public virtual DbSet<KitchenDashboardVm> KitchenDashboardVm { get; set; }
        /// <summary>
        /// KitchenDashboardKitchenVm
        /// </summary>
        public virtual DbSet<KitchenDashboardKitchenVm> KitchenDashboardKitchenVm { get; set; }
        /// <summary>
        /// KitchenCountVm
        /// </summary>
        public virtual DbSet<KitchenCountVm> KitchenCountVm { get; set; }
        /// <summary>
        /// KitchenChartVm
        /// </summary>
        public virtual DbSet<KitchenChartVm> KitchenChartVm { get; set; }
        /// <summary>
        /// KitchenOrderSummaryVm
        /// </summary>
        public virtual DbSet<KitchenOrderSummaryVm> KitchenOrderSummaryVm { get; set; }
        /// <summary>
        /// KitchenUsersVm
        /// </summary>
        public virtual DbSet<KitchenAutoCompleteVm> KitchenAutoCompleteVm { get; set; }
        /// <summary>
        /// KitchenDashboardKitchenVm
        /// </summary>
        public virtual DbSet<KitchenUsersVm> KitchenUsersVm { get; set; }
        /// <summary>
        /// KitchenUserInformationVm
        /// </summary>
        public virtual DbSet<KitchenUserInformationVm> KitchenUserInformationVm { get; set; }
        /// <summary>
        /// KitchenManageOrderVm
        /// </summary>
        public virtual DbSet<KitchenManageOrderVm> KitchenManageOrderVm { get; set; }
        /// <summary>
        /// KitchenManageOrdersReportVm
        /// </summary>
        public virtual DbSet<KitchenManageOrdersReportVm> KitchenManageOrdersReportVm { get; set; }
        /// <summary>
        /// KitchenFoodOrderInvoiceDetailVm
        /// </summary>
        public virtual DbSet<KitchenFoodOrderInvoiceDetailVm> KitchenFoodOrderInvoiceDetailVm { get; set; }
        /// <summary>
        /// KitchenOrderDishesItemVm
        /// </summary>
        public virtual DbSet<KitchenOrderDishesItemVm> KitchenOrderDishesItemVm { get; set; }
        /// <summary>
        /// KitchenDTO
        /// </summary>
        public virtual DbSet<KitchenDTO> KitchenDTO { get; set; }
        /// <summary>
        /// KitchenReviewsVm
        /// </summary>
        public virtual DbSet<KitchenReviewsVm> KitchenReviewsVm { get; set; }
        /// <summary>
        /// KitchenReviewsReplyModel
        /// </summary>
        public virtual DbSet<KitchenReviewsReplyModel> KitchenReviewsReplyModel { get; set; }
        /// <summary>
        /// OrderItemVm
        /// </summary>
        public virtual DbSet<OrderItemVm> OrderItemVm { get; set; }

        /// <summary>
        /// OrderCountVm
        /// </summary>
        public virtual DbSet<OrderCountVm> OrderCountVm { get; set; }
        /// <summary>
        /// RefundItemDetail
        /// </summary>
        public virtual DbSet<RefundItemDetail> RefundItemDetail { get; set; }

        /// <summary>
        /// MessageModel
        /// </summary>
        public virtual DbSet<MessageModel> MessageModel { get; set; }

        /// <summary>
        /// Response
        /// </summary>
        public virtual DbSet<Response> Response { get; set; }

        /// <summary>
        /// FinancialReportVm
        /// </summary>
        public virtual DbSet<FinancialReportVm> FinancialReportVm { get; set; }
        /// <summary>
        /// ManageBalanceKitchenVm
        /// </summary>
        public virtual DbSet<ManageBalanceKitchenVm> ManageBalanceKitchenVm { get; set; }
        /// <summary>
        /// ManageBalanceKitchenInvoiceVm
        /// </summary>
        public virtual DbSet<ManageBalanceKitchenInvoiceVm> ManageBalanceKitchenInvoiceVm { get; set; }
        /// <summary>
        /// MessageResponseVm
        /// </summary>
        public virtual DbSet<MessageResponseVm> MessageResponseVm { get; set; }
        /// <summary>
        /// FinancialMonthYear
        /// </summary>
        public virtual DbSet<FinancialMonthYear> FinancialMonthYear { get; set; }
        /// <summary>
        /// UserVm
        /// </summary>
        public virtual DbSet<UserVm> UserVm { get; set; }
        /// <summary>
        /// KitchenList
        /// </summary>
        public virtual DbSet<KitchenList> KitchenList { get; set; }

        public virtual DbSet<NotificationDTO> NotificationDTO { get; set; }
   

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {}
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<ScCountry>(entity =>
            {
                entity.HasKey(e => e.CountryId)
                    .HasName("PK__CountryM__10D1609F7F60ED59");

                entity.ToTable("SC_Countries");

                entity.Property(e => e.CountryName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DialCode)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.ShortName)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<ScState>(entity =>
            {
                entity.HasKey(e => e.StateId)
                    .HasName("PK__StateMas__C3BA3B3A03317E3D");

                entity.ToTable("SC_States");

                entity.Property(e => e.CategCreateDate).HasColumnType("datetime");

                entity.Property(e => e.CategModifyDate).HasColumnType("datetime");

                entity.Property(e => e.CategPubDate).HasColumnType("datetime");

                entity.Property(e => e.CategUrl).HasMaxLength(200);

                entity.Property(e => e.ShortName).HasMaxLength(6);

                entity.Property(e => e.StateName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SkKitchen>(entity =>
            {
                entity.HasKey(e => e.ProfileId)
                    .HasName("PK__SK_Kitch__290C88E492CEBCAF");

                entity.ToTable("SK_Kitchen");

                entity.Property(e => e.Address1).HasMaxLength(200);

                entity.Property(e => e.Address2).HasMaxLength(200);

                entity.Property(e => e.ApprovalDate).HasColumnType("datetime");

                entity.Property(e => e.BusinessName).HasMaxLength(100);

                entity.Property(e => e.BusinessType).HasMaxLength(50);

                entity.Property(e => e.CityNameOld).HasMaxLength(100);

                entity.Property(e => e.CompanyLegalName).HasMaxLength(100);

                entity.Property(e => e.CountryNameOld).HasMaxLength(100);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerId).HasMaxLength(50);

                entity.Property(e => e.DeliveryCharges).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.DeliveryRadius).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.DeviceId)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.DlfilePath).HasMaxLength(100);

                entity.Property(e => e.Dob)
                    .HasColumnType("datetime")
                    .HasColumnName("DOB");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.FaxNumber).HasMaxLength(25);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.FreeDeliveryLimitAmount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.FullName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Gender).HasMaxLength(15);

                entity.Property(e => e.IpAddress)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("IP_Address");

                entity.Property(e => e.KitchenMessage).HasMaxLength(2000);

                entity.Property(e => e.KitchenName).HasMaxLength(50);

                entity.Property(e => e.Languages).HasMaxLength(200);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Latitude).HasMaxLength(50);

                entity.Property(e => e.Locality).HasMaxLength(200);

                entity.Property(e => e.Longitude).HasMaxLength(50);

                entity.Property(e => e.MacAddress)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Mac_Address");

                entity.Property(e => e.Mi)
                    .HasMaxLength(20)
                    .HasColumnName("MI");

                entity.Property(e => e.MinOrderAmount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.MobileNo).HasMaxLength(25);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.PhoneNo).HasMaxLength(25);

                entity.Property(e => e.ProfileImagePath).HasMaxLength(100);

                entity.Property(e => e.RejectDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.RejectMail).HasDefaultValueSql("((0))");

                entity.Property(e => e.RejectRemarke).HasMaxLength(500);

                entity.Property(e => e.SaleTax).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.SalesTaxPer).HasColumnType("decimal(10, 3)");

                entity.Property(e => e.SrvRejectMail).HasDefaultValueSql("((0))");

                entity.Property(e => e.SrvRejectMailDate).HasColumnType("datetime");

                entity.Property(e => e.Ssn)
                    .HasMaxLength(10)
                    .HasColumnName("SSN");

                entity.Property(e => e.SsnlegalName)
                    .HasMaxLength(100)
                    .HasColumnName("SSNLegalName");

                entity.Property(e => e.Ssnumber)
                    .HasMaxLength(50)
                    .HasColumnName("SSNumber");

                entity.Property(e => e.StateCode).HasMaxLength(2);

                entity.Property(e => e.StateIdfilePath).HasMaxLength(100);

                entity.Property(e => e.StateNameOld).HasMaxLength(100);

                entity.Property(e => e.Status).HasMaxLength(20);

                entity.Property(e => e.TaxId)
                    .HasMaxLength(10)
                    .HasColumnName("TaxID");

                entity.Property(e => e.TaxIdnumber)
                    .HasMaxLength(50)
                    .HasColumnName("TaxIDNumber");

                entity.Property(e => e.UserImagePath).HasMaxLength(100);

                entity.Property(e => e.UserTitle).HasMaxLength(15);

                entity.Property(e => e.W9filePath).HasMaxLength(100);

                entity.Property(e => e.Zipcode).HasMaxLength(10);
            });

            modelBuilder.Entity<SkKitchenRequest>(entity =>
            {
                entity.ToTable("SK_Kitchen_Request");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Reason)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.KitchenProfile)
                    .WithMany(p => p.SkKitchenRequests)
                    .HasForeignKey(d => d.KitchenProfileId)
                    .HasConstraintName("FK__SK_Kitche__Kitch__23C93658");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.SkKitchenRequests)
                    .HasForeignKey(d => d.Status)
                    .HasConstraintName("FK__SK_Kitche__Statu__24BD5A91");
            });

            modelBuilder.Entity<SkKitchenRequestStatus>(entity =>
            {
                entity.ToTable("SK_Kitchen_Request_Status");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SuUser>(entity =>
            {
                entity.ToTable("SU_User");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BusinessLogo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BusinessName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Contact)
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.TaxId)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
