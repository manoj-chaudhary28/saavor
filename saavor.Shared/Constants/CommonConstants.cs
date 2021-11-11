 

namespace saavor.Shared.Constants
{
    public static class CommonConstants
    {
        public static string BusinessLogoFolderName = "businesslogo";
        public static string UserIdentity = "User Identity";
        public static string CookieSaavorUsers = "SaavorUsers";
        public static string SaavorUserId = "SaavorUserId";
        public static string SaavorUserEmail = "SaavorUserEmail";
        public static string BusinessName = "BusinessName";
        public static string UserName = "UserName";
        public static string ProfileId = "ProfileId";
        public static string KitchenName = "KitchenName";
        public static string Zero = "0";
        public static string Kitchen = "Kitchen";
        public static string User = "User";
        public static string EncryptedProfileId = "EncryptedProfileId";
        public static string LoginType = "LoginType";
        public static string ForgotPassword = "Forgot password";
        public static string ForgotPasswordTemplate = "template/forgotpassword.html";
        public static string EmailSent = "Email sent successfully!";
        public static string IsAprovedKitchenRequest = "IsAprovedKitchenRequest";
        public static string InvoiceToPDFTemplate = "template/InvoicetoPDF.html";

        #region Route
        public const string RoutRefund = "refund";
        public const string RoutMessages = "messages";
        public const string RoutMessage = "message";
        public const string RoutAllOrders = "allorders";
        public const string RoutNewOrders = "orders/new";
        public const string RoutPickupOrders = "orders/pickup";
        public const string RoutDeliveryOrders = "orders/delivery";
        public const string RoutReadyOrders = "orders/ready";
        public const string RoutPreparingOrders = "orders/preparing";
        public const string RoutCompletedOrders = "orders/completed";
        public const string RoutRejectedOrders = "orders/rejected";
        public const string RoutOrderReport = "report/order";
        public const string RoutFinancialReport = "report/financial";
        public const string RoutError = "error";
        public const string RoutUserProfile = "profile";

        public const string RoutManageKitchen = "manage/kitchen";
        public const string RoutManageCuisines = "manage/cuisines";
        public const string RoutManageMenuItems = "manage/menuitems";
        #endregion
    }
}
