using Microsoft.Data.SqlClient;
using saavor.ApplicationDapper.GenericRepository;
using saavor.Shared.DTO.Kitchen;
using saavor.Shared.Interfaces;
using saavor.Shared.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace saavor.Application.Kitchen.Commands.UpsertKitchenRequest
{
    public class UpsertKitchenRequestCommand : IUpsertKitchenRequestCommand
    {
        private readonly IRepository<CommonVm> query;
        public UpsertKitchenRequestCommand(IRepository<CommonVm> queryInstance)
        {
            query = queryInstance;
        }
        public async Task<CommonVm> KitchenRequest(KitchenRequestDTO inputDTO)
        {
            return await query.ExecuteProcedureSingle(new List<SqlParameter>(){
                         new SqlParameter("@ProfileId",inputDTO.ProfileId),
                         new SqlParameter("@UserId",inputDTO.UserId),
                         new SqlParameter("@Status",Convert.ToString(inputDTO.Status)),
             }, "SK_Kitchen_Request_Insert").ConfigureAwait(false);
        }
    }
}

