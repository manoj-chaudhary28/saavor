using Microsoft.Data.SqlClient;
using saavor.ApplicationDapper.GenericRepository;
using saavor.Shared.DTO.Kitchen;
using saavor.Shared.DTO.ManageUsers;
using saavor.Shared.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace saavor.Application.Kitchen.Query
{
    /// <summary>
    /// GetKitchenDashboardQuery
    /// </summary>
    public class GetKitchenDashboardQuery
    {
        private readonly IRepository<KitchenDashboardVm> _query;
        private readonly IRepository<KitchenCountVm> _queryCount;
        private readonly IRepository<KitchenDashboardKitchenVm> _queryKitchen;
        private readonly IRepository<KitchenChartVm> _queryKitchenRevenueChart;
        private readonly IRepository<KitchenOrderSummaryVm> _queryKitchenOrderSummaryChart;
        private readonly IRepository<KitchenAutoCompleteVm> _queryKitchenAutoComplete;
        private readonly IRepository<KitchenUsersVm> _queryKitchenUsers;
        private readonly IRepository<KitchenUserInformationVm> _queryKitchenUsersInformation;
        private readonly IRepository<KitchenManageOrderVm> _queryKitchenOrders;
        private readonly IRepository<CommonVm> _queryNonExecute;
        private readonly IRepository<KitchenManageOrdersReportVm> _queryKitchenOrdersReport;
        private readonly IRepository<KitchenFoodOrderInvoiceDetailVm> _queryKitchenOrdersInvoice;
        private readonly IRepository<KitchenOrderDishesItemVm> _queryKitchenDishesOrdersInvoice;
        private readonly IRepository<KitchenReviewsVm> _queryKitchenReviews;
        private readonly IRepository<KitchenReviewsReplyModel> _queryKitchenReviewsReply;
        private readonly IRepository<OrderItemVm> _queryOrderItem;
        private readonly IRepository<OrderCountVm> _queryOrderCount;
        private readonly IRepository<RefundItemDetail> _queryRefundItemDetail;
        private readonly IRepository<Response> _queryCommonResponse;

        /// <summary>
        /// GetKitchenDashboardQuery
        /// </summary>
        /// <param name="queryInstance"></param>
        /// <param name="countQueryInstance"></param>
        /// <param name="queryKitchenInstance"></param>
        /// <param name="queryKitchenChartInstance"></param>
        /// <param name="queryKitchenOrderSummaryChartInstance"></param>
        /// <param name="queryKitchenAutoCompleteInstance"></param>
        /// <param name="queryKitchenUsersInstance"></param>
        /// <param name="queryKitchenUsersInformationInstance"></param>
        /// <param name="queryKitchenOrdersInstance"></param>
        /// <param name="queryNonExecuteInstance"></param>
        /// <param name="queryKitchenOrdersReportInstance"></param>
        /// <param name="queryKitchenOrdersInvoiceInstance"></param>
        /// <param name="queryKitchenDishesOrdersInvoiceInstance"></param>
        /// <param name="queryKitchenReviewsInstance"></param>
        /// <param name="queryKitchenReviewsReplyInstance"></param>
        /// <param name="queryOrderItem"></param>
        /// <param name="queryOrderCount"></param>
        /// <param name="queryRefundItemDetailInstance"></param>
        /// <param name="queryCommonResponseInstance"></param>
        public GetKitchenDashboardQuery(IRepository<KitchenDashboardVm> queryInstance
                                        ,IRepository<KitchenCountVm> countQueryInstance
                                        ,IRepository<KitchenDashboardKitchenVm> queryKitchenInstance
                                        ,IRepository<KitchenChartVm> queryKitchenChartInstance
                                        ,IRepository<KitchenOrderSummaryVm> queryKitchenOrderSummaryChartInstance
                                        ,IRepository<KitchenAutoCompleteVm> queryKitchenAutoCompleteInstance
                                        ,IRepository<KitchenUsersVm> queryKitchenUsersInstance
                                        ,IRepository<KitchenUserInformationVm> queryKitchenUsersInformationInstance
                                        ,IRepository<KitchenManageOrderVm> queryKitchenOrdersInstance
                                        ,IRepository<CommonVm> queryNonExecuteInstance
                                        ,IRepository<KitchenManageOrdersReportVm> queryKitchenOrdersReportInstance
                                        ,IRepository<KitchenFoodOrderInvoiceDetailVm> queryKitchenOrdersInvoiceInstance
                                        ,IRepository<KitchenOrderDishesItemVm> queryKitchenDishesOrdersInvoiceInstance
                                        ,IRepository<KitchenReviewsVm> queryKitchenReviewsInstance
                                        ,IRepository<KitchenReviewsReplyModel> queryKitchenReviewsReplyInstance
                                        ,IRepository<OrderItemVm> queryOrderItem
                                        ,IRepository<OrderCountVm> queryOrderCount
                                        ,IRepository<RefundItemDetail> queryRefundItemDetailInstance
                                        ,IRepository<Response> queryCommonResponseInstance)
        {
            _query = queryInstance;
            _queryCount = countQueryInstance;
            _queryKitchen = queryKitchenInstance;
            _queryKitchenRevenueChart = queryKitchenChartInstance;
            _queryKitchenOrderSummaryChart = queryKitchenOrderSummaryChartInstance;
            _queryKitchenAutoComplete = queryKitchenAutoCompleteInstance;
            _queryKitchenUsers = queryKitchenUsersInstance;
            _queryKitchenUsersInformation = queryKitchenUsersInformationInstance;
            _queryKitchenOrders = queryKitchenOrdersInstance;
            _queryNonExecute = queryNonExecuteInstance;
            _queryKitchenOrdersReport = queryKitchenOrdersReportInstance;
            _queryKitchenOrdersInvoice = queryKitchenOrdersInvoiceInstance;
            _queryKitchenDishesOrdersInvoice = queryKitchenDishesOrdersInvoiceInstance;
            _queryKitchenReviews = queryKitchenReviewsInstance;
            _queryKitchenReviewsReply = queryKitchenReviewsReplyInstance;
            _queryOrderItem = queryOrderItem;
            _queryOrderCount = queryOrderCount;
            _queryRefundItemDetail = queryRefundItemDetailInstance;
            _queryCommonResponse = queryCommonResponseInstance;

        }
        //public async Task<KitchenDashboardVm> KitchenDashboard(saavor.Shared.DTO.Kitchen.KitchenDTO inputDTO)
        //{
        //    var response = await query.MulitpleResultSetDapper(new List<SqlParameter>(){
        //                 new SqlParameter("@UserId",inputDTO.UserId),
        //                 new SqlParameter("@Page",inputDTO.Page),
        //                 new SqlParameter("@Size",inputDTO.Size),
        //     }, "SU_Kitchen_Dashboard_Get", null, 0);

        //    return new KitchenDashboardVm();
        //}
        /// <summary>
        /// KitchenDashboardCount
        /// </summary>
        /// <param name="inputDTO"></param>
        /// <returns></returns>
        public async Task<KitchenCountVm> KitchenDashboardCount(KitchenInputDTO inputDTO)
        {
            return await _queryCount.ExecuteProcedureSingle(new List<SqlParameter>(){
                         new SqlParameter("@UserId",inputDTO.UserId),
                         new SqlParameter("@ProfileId",inputDTO.ProfileId)
             }, "SU_Kitchen_Dashboard_Count_Get");
        }
        /// <summary>
        /// KitchenDashboardKitchens
        /// </summary>
        /// <param name="inputDTO"></param>
        /// <returns></returns>
        public async Task<List<KitchenDashboardKitchenVm>> KitchenDashboardKitchens(KitchenInputDTO inputDTO)
        {
            return await _queryKitchen.ExecuteProcedureList(new List<SqlParameter>(){
                         new SqlParameter("@UserId",inputDTO.UserId),
                         new SqlParameter("@Page",inputDTO.Page),
                         new SqlParameter("@Size",inputDTO.Size),
                         new SqlParameter("@KitchenId",inputDTO.KitchenId)
             }, "SU_Kitchen_Get");
        }
        /// <summary>
        /// KitchenRevenueChart
        /// </summary>
        /// <param name="inputDTO"></param>
        /// <returns></returns>
        public async Task<List<KitchenChartVm>> KitchenRevenueChart(KitchenInputDTO inputDTO)
        {
            return await _queryKitchenRevenueChart.ExecuteProcedureList(new List<SqlParameter>(){
                         new SqlParameter("@UserId",inputDTO.UserId),
                          new SqlParameter("@ProfileId",inputDTO.ProfileId)
             }, "SU_Kitchen_Dashboard_Revenue_Chart_Get");
        }
        /// <summary>
        /// KitchenOrderSummaryChart
        /// </summary>
        /// <param name="inputDTO"></param>
        /// <returns></returns>
        public async Task<KitchenOrderSummaryVm> KitchenOrderSummaryChart(KitchenInputDTO inputDTO)
        {
            return await _queryKitchenOrderSummaryChart.ExecuteProcedureSingle(new List<SqlParameter>(){
                         new SqlParameter("@UserId",inputDTO.UserId),
                         new SqlParameter("@ProfileId",inputDTO.ProfileId),
                         new SqlParameter("@Type",inputDTO.Type)
             }, "SU_Kitchen_Dashboard_OrderSummary_Chart_Get");
        }
        /// <summary>
        /// KitchenAutoComplete
        /// </summary>
        /// <param name="inputDTO"></param>
        /// <returns></returns>
        public async Task<List<KitchenAutoCompleteVm>> KitchenAutoComplete(KitchenInputDTO inputDTO)
        {
            return await _queryKitchenAutoComplete.ExecuteProcedureList(new List<SqlParameter>(){
                         new SqlParameter("@UserId",inputDTO.UserId),
                         new SqlParameter("@Search",inputDTO.Search)
             }, "SU_Kitchen_AutoComplete_Get");
        }

        /// <summary>
        /// KitchenUsers
        /// </summary>
        /// <param name="inputDTO"></param>
        /// <returns></returns>
        public async Task<List<KitchenUsersVm>> KitchenUsers(ManageUserInputDTO inputDTO)
        {
            return await _queryKitchenUsers.ExecuteProcedureList(new List<SqlParameter>(){
                         new SqlParameter("@UserId",inputDTO.UserId),
                         new SqlParameter("@Page",inputDTO.Page),
                         new SqlParameter("@Size",inputDTO.Size),
                         new SqlParameter("@ProfileId",inputDTO.ProfileId),
                         new SqlParameter("@Search",inputDTO.Search ?? string.Empty)
             }, "SU_Kitchen_Manage_User_Get");
        }

        /// <summary>
        /// KitchenUserInformationById
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<KitchenUserInformationVm> KitchenUserInformationById(Int64 userId)
        {
            return await _queryKitchenUsersInformation.ExecuteProcedureSingle(new List<SqlParameter>(){
                         new SqlParameter("@UserId",userId)
             }, "SU_Kitchen_Manage_User_Get_ById");
        }

        /// <summary>
        /// KitchenManageOrders
        /// </summary>
        /// <param name="inputDTO"></param>
        /// <returns></returns>
         
        public async Task<List<KitchenManageOrderVm>> KitchenManageOrders(KitchenInputDTO inputDTO)
        {
            return await _queryKitchenOrders.ExecuteProcedureList(new List<SqlParameter>(){
                         new SqlParameter("@UserId",inputDTO.UserId),
                         new SqlParameter("@Page",inputDTO.Page),
                         new SqlParameter("@Size",inputDTO.Size),
                         new SqlParameter("@ProfileId",inputDTO.ProfileId),
                         new SqlParameter("@Search",inputDTO.Search ?? string.Empty),
                         new SqlParameter("@OrderType",inputDTO.OrderType)
             }, "SU_Kitchen_Manage_Order_Get");
        }
        /// <summary>
        /// KitchenManageOrdersPickupDelivery
        /// </summary>
        /// <param name="inputDTO"></param>
        /// <returns></returns>
         
        public async Task<List<KitchenManageOrderVm>> KitchenManageOrdersPickupDelivery(KitchenInputDTO inputDTO)
        {
            return await _queryKitchenOrders.ExecuteProcedureList(new List<SqlParameter>(){
                         new SqlParameter("@UserId",inputDTO.UserId),
                         new SqlParameter("@Page",inputDTO.Page),
                         new SqlParameter("@Size",inputDTO.Size),
                         new SqlParameter("@ProfileId",inputDTO.ProfileId),
                         new SqlParameter("@Search",inputDTO.Search ?? string.Empty),
                         new SqlParameter("@OrderType",inputDTO.OrderType),
                         new SqlParameter("@IsDeliver",inputDTO.IsDeliver)
             }, "SU_Kitchen_Manage_Order_Get_Pickup_Deliver");
        }
        /// <summary>
        /// KitchenManageOrdersUpdateStatus
        /// </summary>
        /// <param name="foodOrderId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<CommonVm> KitchenManageOrdersUpdateStatus(string foodOrderIds, string status, string reason)
        {
            return await _queryNonExecute.ExecuteProcedureSingle(new List<SqlParameter>(){
                         new SqlParameter("@FoodOrderId",foodOrderIds),
                         new SqlParameter("@Status",status),
                          new SqlParameter("@Reason",reason),
             }, "SU_Kitchen_Manage_Order_Update_Status");

        }
        /// <summary>
        /// KitchenManageOrdersReport
        /// </summary>
        /// <param name="inputDTO"></param>
        /// <returns></returns>
        public async Task<List<KitchenManageOrdersReportVm>> KitchenManageOrdersReport(KitchenInputDTO inputDTO)
        {
            return await _queryKitchenOrdersReport.ExecuteProcedureList(new List<SqlParameter>(){
                         new SqlParameter("@UserId",inputDTO.UserId),
                         new SqlParameter("@Page",inputDTO.Page),
                         new SqlParameter("@Size",inputDTO.Size),
                         new SqlParameter("@ProfileId",inputDTO.ProfileId),
                         new SqlParameter("@StartDate",inputDTO.FromDate ?? string.Empty),
                         new SqlParameter("@EndDate",inputDTO.ToDate ?? string.Empty)
             }, "SU_Kitchen_Manage_Order_Reports_Get");
        }
        /// <summary>
        /// KitchenOrderInvoice
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        
        public async Task<KitchenFoodOrderInvoiceDetailVm> KitchenOrderInvoice(Int64 orderId)
        {
            return await _queryKitchenOrdersInvoice.ExecuteProcedureSingle(new List<SqlParameter>(){
                         new SqlParameter("@FoodOrderId",orderId)
             }, "SU_Kitchen_Order_Invoice_Get");
        }

        /// <summary>
        /// KitchenOrderDishesInvoice
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
      
        public async Task<List<KitchenOrderDishesItemVm>> KitchenOrderDishesInvoice(Int64 orderId)
        {
            return await _queryKitchenDishesOrdersInvoice.ExecuteProcedureList(new List<SqlParameter>(){
                         new SqlParameter("@FoodOrderId",orderId)
             }, "SU_Kitchen_Order_Invoice_Dishes_Get");
        }

        /// <summary>
        /// KitchenManageOrdersReportToExport
        /// </summary>
        /// <param name="inputDTO"></param>
        /// <returns></returns>
        public async Task<List<KitchenManageOrdersReportVm>> KitchenManageOrdersReportToExport(KitchenInputDTO inputDTO)
        {
            return await _queryKitchenOrdersReport.ExecuteProcedureList(new List<SqlParameter>(){
                         new SqlParameter("@UserId",inputDTO.UserId),
                         new SqlParameter("@ProfileId",inputDTO.ProfileId),
                         new SqlParameter("@StartDate",inputDTO.FromDate),
                         new SqlParameter("@EndDate",inputDTO.ToDate)
             }, "SU_Kitchen_Manage_Order_Reports_Get_Export");
        }

        /// <summary>
        /// KitchenDelete
        /// </summary>
        /// <param name="inputDTO"></param>
        /// <returns></returns>
        public async Task<CommonVm> KitchenDelete(KitchenInputDTO inputDTO)
        {
            return await _queryNonExecute.ExecuteProcedureSingle(new List<SqlParameter>(){
                   new SqlParameter("@ProfileId",inputDTO.ProfileId),
                        new SqlParameter("@UserId",inputDTO.UserId)                      
             }, "SU_Kitchen_Removed");
        }

        /// <summary>
        /// KitchenReviews
        /// </summary>
        /// <param name="inputDTO"></param>
        /// <returns></returns>
        /// FN_SU_Kitchen_AvarageRating 
        public async Task<List<KitchenReviewsVm>> KitchenReviews(KitchenInputDTO inputDTO)
        {
            return await _queryKitchenReviews.ExecuteProcedureList(new List<SqlParameter>(){
                         new SqlParameter("@ProfileId",inputDTO.ProfileId),
                         new SqlParameter("@FromDate",inputDTO.FromDate),
                         new SqlParameter("@ToDate",inputDTO.ToDate),
                         new SqlParameter("@Page",inputDTO.Page),
                         new SqlParameter("@Size",inputDTO.Size)
             }, "SU_KitchenReviews_Get");
        }

        /// <summary>
        /// KitchenReviewDelete
        /// </summary>
        /// <param name="inputDTO"></param>
        /// <returns></returns>
        public async Task<CommonVm> KitchenReviewDelete(KitchenInputDTO inputDTO)
        {
            return await _queryNonExecute.ExecuteProcedureSingle(new List<SqlParameter>(){
                   new SqlParameter("@ReviewId",inputDTO.ReviewId)
             }, "SU_KitchenReviews_Delete");
        }

        /// <summary>
        /// KitchenReviewReply
        /// </summary>
        /// <param name="reply"></param>
        /// <param name="reviewId"></param>
        /// <returns></returns>
        public async Task<CommonVm> KitchenReviewReply(string reply, Int64 reviewId)
        {
            return await _queryNonExecute.ExecuteProcedureSingle(new List<SqlParameter>(){
                         new SqlParameter("@ReviewId",reviewId),
                         new SqlParameter("@ReplyMessage",reply)
             }, "SU_Review_Reply_Insert");

        }

        /// <summary>
        /// KitchenReviewReplyGet
        /// </summary>
        /// <param name="reviewId"></param>
        /// <returns></returns>
        public async Task<List<KitchenReviewsReplyModel>> KitchenReviewReplyGet( Int64 reviewId)
        {
            return await _queryKitchenReviewsReply.ExecuteProcedureList(new List<SqlParameter>(){
                         new SqlParameter("@ReviewId",reviewId)
             }, "SU_Review_Reply_Get");

        }

        /// <summary>
        /// GetItemRefunds
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
         
        public async Task<List<OrderItemVm>> GetItemRefunds(Int64 orderId)
        {
            return await _queryOrderItem.ExecuteProcedureList(new List<SqlParameter>(){
                         new SqlParameter("@FoodOrderId",orderId)
             }, "SU_Kitchen_Order_Refund_Dishes_Get");
        }

        /// <summary>
        /// GetItemDetail
        /// </summary>
        /// <param name="foodOrderDetailId"></param>
        /// <returns></returns>
        public async Task<RefundItemDetail> GetItemDetail(Int64 foodOrderDetailId)
        {
            return await _queryRefundItemDetail.ExecuteProcedureSingle(new List<SqlParameter>(){
                         new SqlParameter("@FoodOrderDetailId",foodOrderDetailId)
             }, "SU_Kitchen_Order_Refund_Dish_Detail_Get");
        }

        /// <summary>
        /// GetOrdersCount
        /// </summary>
        /// <param name="profileId"></param>
        /// <returns></returns>
        public async Task<OrderCountVm> GetOrdersCount(Int64 profileId)
        {
            return await _queryOrderCount.ExecuteProcedureSingle(new List<SqlParameter>(){
                         new SqlParameter("@ProfileId",profileId)
             }, "SU_Get_Orders_Count");
        }

        /// <summary>
        /// UpdateOrderStatus
        /// </summary>
        /// <param name="inputDTO"></param>
        /// <returns></returns>
        
        public async Task<Response> UpdateOrderStatus(FoodOrderStatusDTO inputDTO)
        {
            return await _queryCommonResponse.ExecuteProcedureSingle(new List<SqlParameter>(){
                         new SqlParameter("@ProfileId",inputDTO.ProfileId),
                         new SqlParameter("@FoodOrderIds",inputDTO.FoodOrderIds),
                         new SqlParameter("@OrderStatus",inputDTO.OrderStatus),
                         new SqlParameter("@DeliveryBy",string.Empty),
                         new SqlParameter("@CurrentDate",inputDTO.SystemDateTime),
                         new SqlParameter("@RejectReason",inputDTO.RejectReason ?? string.Empty),
             }, "SU_FoodOrdersAcceptReject");

        }

    }
}
