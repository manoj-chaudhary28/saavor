using Microsoft.Data.SqlClient;
using saavor.ApplicationDapper.GenericRepository;
using saavor.Shared.DTO.Kitchen;
using saavor.Shared.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace saavor.Application.BalanceKitchen.Query
{
    /// <summary>
    /// GetKitchenBalanceQuery
    /// </summary>
    public class GetKitchenBalanceQuery
    {
        /// <summary>
        /// queryFinancialReport
        /// </summary>
        private readonly IRepository<FinancialReportVm> queryFinancialReport;

        /// <summary>
        /// queryManageBalanceKitchen
        /// </summary>
        private readonly IRepository<ManageBalanceKitchenVm> queryManageBalanceKitchen;

        /// <summary>
        /// queryManageBalanceKitchenInvoiceVm
        /// </summary>
        private readonly IRepository<ManageBalanceKitchenInvoiceVm> queryManageBalanceKitchenInvoiceVm;

        /// <summary>
        /// queryFinancialMonthYear
        /// </summary>
        private readonly IRepository<FinancialMonthYear> queryFinancialMonthYear;

        /// <summary>
        /// queryKitchenList
        /// </summary>
        private readonly IRepository<KitchenList> queryKitchenList;

        /// <summary>
        /// GetKitchenBalanceQuery
        /// </summary>
        /// <param name="queryInstance"></param>
        public GetKitchenBalanceQuery(IRepository<ManageBalanceKitchenVm> queryManageBalanceKitchenInstance
                                      ,IRepository<FinancialReportVm> queryFinancialReportInstance
                                      ,IRepository<ManageBalanceKitchenInvoiceVm> queryManageBalanceKitchenInvoiceVmInstance
                                      ,IRepository<FinancialMonthYear> queryFinancialMonthYearInstance
                                      ,IRepository<KitchenList> queryKitchenListInstance)
        {
            queryFinancialReport = queryFinancialReportInstance;
            queryManageBalanceKitchen = queryManageBalanceKitchenInstance;
            queryManageBalanceKitchenInvoiceVm = queryManageBalanceKitchenInvoiceVmInstance;
            queryFinancialMonthYear = queryFinancialMonthYearInstance;
            queryKitchenList = queryKitchenListInstance;
        }

        /// <summary>
        /// GetFinancialMonthYear
        /// </summary>
        /// <param name="inputDTO"></param>
        /// <returns></returns>
        
        public async Task<List<FinancialMonthYear>> GetFinancialMonthYear(KitchenInputDTO inputDTO)
        {
            return await queryFinancialMonthYear.ExecuteProcedureList(new List<SqlParameter>(){
                         new SqlParameter("@ProfileIds",inputDTO.MultiIds),
                         new SqlParameter("@StartDate",inputDTO.FromDate ?? string.Empty),
                          new SqlParameter("@EndDate",inputDTO.ToDate ?? string.Empty)
             }, "SU_Get_Financial_MonthYear");
        }

        /// <summary>
        /// FinancialReport
        /// </summary>
        /// <param name="inputDTO"></param>
        /// <returns></returns>
        
        public async Task<List<FinancialReportVm>> GetFinancialReport(KitchenInputDTO inputDTO)
        {
            return await queryFinancialReport.ExecuteProcedureList(new List<SqlParameter>(){
                         new SqlParameter("@ProfileIds",inputDTO.MultiIds),
                         new SqlParameter("@StartDate",inputDTO.FromDate ?? string.Empty),
                         new SqlParameter("@EndDate",inputDTO.ToDate ?? string.Empty)
             }, "SU_Get_Financial");
        }
        
        /// <summary>
        /// GetManagebalance
        /// </summary>
        /// <param name="inputDTO"></param>
        /// <returns></returns>
         
        public async Task<List<ManageBalanceKitchenVm>> GetManagebalance(KitchenInputDTO inputDTO)
        {
            return await queryManageBalanceKitchen.ExecuteProcedureList(new List<SqlParameter>(){
                         new SqlParameter("@ProfileId",inputDTO.ProfileId),
                         new SqlParameter("@StartDate",inputDTO.FromDate ?? string.Empty),
                          new SqlParameter("@EndDate",inputDTO.ToDate ?? string.Empty)
             }, "SU_Passbook_Select");
        }

        /// <summary>
        /// GetManagebalanceInvoice
        /// </summary>
        /// <param name="inputDTO"></param>
        /// <returns></returns>
        
        public async Task<ManageBalanceKitchenInvoiceVm> GetManagebalanceInvoice(KitchenInputDTO inputDTO)
        {
            return await queryManageBalanceKitchenInvoiceVm.ExecuteProcedureSingle(new List<SqlParameter>(){
                         new SqlParameter("@ProfileId",inputDTO.ProfileId),
                         new SqlParameter("@CreateDate",inputDTO.Search ?? string.Empty)                          
             }, "SU_Passbook_Invoice_Select");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputDTO"></param>
        /// <returns></returns>
       
        public async Task<List<KitchenList>> GetKitchens(KitchenInputDTO inputDTO)
        {
            return await queryKitchenList.ExecuteProcedureList(new List<SqlParameter>(){
                         new SqlParameter("@UserId",inputDTO.UserId),
                         new SqlParameter("@ProfileId",inputDTO.ProfileId)                         
             }, "SU_Get_Kitchen_For_Financial_Report");
        }


    }
}
