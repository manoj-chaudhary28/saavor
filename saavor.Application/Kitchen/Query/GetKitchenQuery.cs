using saavor.ApplicationDapper.GenericRepository;
using saavor.EntityFrameworkCore.Context;
using saavor.Shared.Enumrations;
using saavor.Shared.ViewModel;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace saavor.Application.Kitchen.Query
{
    public class GetKitchenQuery
    {
        private readonly ApplicationDBContext context;
        private readonly IRepository<KitchenDTO> _query;
        public GetKitchenQuery(ApplicationDBContext dbContextInstance, IRepository<KitchenDTO> queryInstance)
        {
            context = dbContextInstance;
            _query = queryInstance;
        }
        /// <summary>
        /// Load the kitchen to request
        /// </summary>
        /// <returns></returns>
        public async Task<List<KitchenDTO>> GetKitchen(Int64 userId,Int64 profileId,Int32 page, Int32 size)
        {
            return await _query.ExecuteProcedureList(new List<SqlParameter>(){
                         new SqlParameter("@UserId",userId),
                         new SqlParameter("@ProfileId",profileId),
                         new SqlParameter("@Page",page),
                         new SqlParameter("@Size",size)
             }, "SU_Kitchen_Get_To_Request");
            //return (from kitchen in context.SkKitchens
            //        join request in context.SkKitchenRequests on kitchen.ProfileId equals request.KitchenProfileId into request
            //        from kitchenrequest in request.Where(x => x.StatusNavigation.Status == Convert.ToString(KitchenRequestStatusEnum.Pending))
            //        .DefaultIfEmpty()
            //        orderby kitchen.KitchenId descending
            //        select new KitchenDTO
            //        {
            //            ProfileId = kitchen.ProfileId,
            //            KitchenId = kitchen.KitchenId,
            //            KitchenName = kitchen.KitchenName,
            //            KitchenAddress = kitchen.Address1,
            //            RequestId = kitchenrequest.Id
            //        }
            //);
        }
        /// <summary>
        /// Load the kitchen to Auto Complete
        /// </summary>
        /// <param name="kitchenId"></param>
        /// <returns></returns>
        public async Task<List<KitchenDTO>> GetKitchenAutoComplete(Int64 userId,string kitchenId)
        {
            return await _query.ExecuteProcedureList(new List<SqlParameter>(){
                         new SqlParameter("@UserId",userId),
                         new SqlParameter("@KitchenId",kitchenId),
             }, "SU_Kitchen_Get_Request_AutoComplete");

            //return (from kitchen in context.SkKitchens
            //        join request in context.SkKitchenRequests on kitchen.ProfileId equals request.KitchenProfileId into request
            //        from kitchenrequest in request.Where(x => x.StatusNavigation.Status == Convert.ToString(KitchenRequestStatusEnum.Pending))
            //        .DefaultIfEmpty()
            //        where kitchen.KitchenId.ToString().Contains(kitchenId)
            //        orderby kitchen.KitchenId descending
            //        select new KitchenDTO
            //        {
            //            KitchenId = kitchen.KitchenId,
            //            KitchenName = kitchen.KitchenName ?? string.Empty
            //        }
            //);
        }
    }
}