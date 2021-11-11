using saavor.ApplicationDapper.GenericRepository;
using saavor.EntityFrameworkCore.Context;
using saavor.Shared.Enumrations;
using saavor.Shared.ViewModel;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using saavor.Shared.DTO.Message;

namespace saavor.Application.Messages.Query
{
    /// <summary>
    /// GetMessageQuery
    /// </summary>
    public class GetMessageQuery
    {
        /// <summary>
        /// _query
        /// </summary>
        private readonly IRepository<MessageModel> _query;

        /// <summary>
        /// GetMessageQuery
        /// </summary>
        /// <param name="queryInstance"></param>
        public GetMessageQuery(IRepository<MessageModel> queryInstance)
        {            
            _query = queryInstance;
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="input"></param>
        /// <returns>MessageModel</returns>
        public async Task<List<MessageModel>> Get(MessageDTO input)
        {
            return await _query.ExecuteProcedureList(new List<SqlParameter>(){
                         new SqlParameter("@ProfileId",input.ProfileId),
                         new SqlParameter("@Page",input.Page),
                         new SqlParameter("@Size",input .Size),
                         new SqlParameter("@Search",input .Search ?? string.Empty),
             }, "SU_Messages_Get");
             
        }

        /// <summary>
        /// GetById
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<MessageModel> GetById(MessageDTO input)
        {
            return await _query.ExecuteProcedureSingle(new List<SqlParameter>(){
                         new SqlParameter("@ProfileId",input.ProfileId),
                         new SqlParameter("@MessageId",input.MessageId)                         
             }, "SU_Messages_Get_By_Id");

        }
    }
}
