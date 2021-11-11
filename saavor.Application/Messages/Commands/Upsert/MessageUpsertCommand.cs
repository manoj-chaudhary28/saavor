using Microsoft.Data.SqlClient;
using saavor.ApplicationDapper.GenericRepository;
using saavor.Shared.DTO.Message;
using saavor.Shared.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace saavor.Application.Messages.Commands.Upsert
{
    /// <summary>
    /// MessageUpsertCommand
    /// </summary>
    public class MessageUpsertCommand
    {
        /// <summary>
        /// query
        /// </summary>
        private readonly IRepository<Response> query;
        /// <summary>
        /// queryMessage
        /// </summary>
        private readonly IRepository<MessageResponseVm> queryMessage;

        /// <summary>
        /// MessageUpsertCommand
        /// </summary>
        /// <param name="queryInstance"></param>
        public MessageUpsertCommand(IRepository<Response> queryInstance
                                    ,IRepository<MessageResponseVm> queryMessageInstance)
        {
            query = queryInstance;
            queryMessage = queryMessageInstance;


        }

        /// <summary>
        /// Upsert
        /// </summary>
        /// <param name="inputDTO"></param>
        /// <returns>MessageVm</returns>
        public async Task<List<MessageResponseVm>> Upsert(MessageDTO inputDTO)
        {
            return await queryMessage.ExecuteProcedureList(new List<SqlParameter>(){
                         new SqlParameter("@MessageId",inputDTO.MessageId),
                         new SqlParameter("@ProfileId",inputDTO.ProfileId),
                         new SqlParameter("@Subject",inputDTO.Subject ?? string.Empty),
                         new SqlParameter("@Message",inputDTO.Message ?? string.Empty),
                         new SqlParameter("@NotificationType",inputDTO.NotificationType ?? string.Empty),
             }, "SU_Messages_Insert").ConfigureAwait(false);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="inputDTO"></param>
        /// <returns>CommonVm</returns>
        public async Task<Response> Delete(MessageDTO inputDTO)
        {
            return await query.ExecuteProcedureSingle(new List<SqlParameter>(){
                         new SqlParameter("@MessageId",inputDTO.MessageId),
                         new SqlParameter("@ProfileId",inputDTO.ProfileId)               
             }, "SU_Messages_Delete").ConfigureAwait(false);
        }
    }
}
