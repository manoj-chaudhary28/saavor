using Microsoft.Data.SqlClient;
using saavor.ApplicationDapper.GenericRepository;
using saavor.EntityFrameworkCore.Context;
using saavor.Shared.DTO.Notification;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace saavor.Application.Notification.Query
{
    /// <summary>
    /// NotificationQuery
    /// </summary>
    public class NotificationQuery
    {
        /// <summary>
        /// context
        /// </summary>
        private readonly ApplicationDBContext context;

        /// <summary>
        /// query
        /// </summary>
        private readonly IRepository<NotificationDTO> query;

        /// <summary>
        /// NotificationQuery
        /// </summary>
        /// <param name="dbContextInstance"></param>
        /// <param name="queryInstance"></param>
        public NotificationQuery( ApplicationDBContext dbContextInstance
                                  ,IRepository<NotificationDTO> queryInstance)
        {
            context = dbContextInstance;
            query = queryInstance;
        }
        /// <summary>
        /// GetNotification
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<NotificationDTO>> GetNotification(NotificationInputDTO input)
        {
            return await query.ExecuteProcedureList(new List<SqlParameter>(){
                         new SqlParameter("@UserId",input.UserId),
                         new SqlParameter("@ProfileId",input.ProfileId)                      
             }, "SU_Kitchen_Orders_Notification_Get");
             
        }
    }
}
