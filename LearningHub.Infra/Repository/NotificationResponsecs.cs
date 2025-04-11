using Dapper;
using LearningHub.Core.Common;
using LearningHub.Core.Repository;
using LearningHub.Core.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace LearningHub.Infra.Repository
{
    public class NotificationResponsecs : INotificationRepository
    {
        private readonly IDbContext _dbContext;
        private readonly  IConfiguration _configuration;

        public NotificationResponsecs(IDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public void CreateNotification(Notification notification)
        {
            var p = new DynamicParameters();
            p.Add("Nuserid", notification.userId,dbType:DbType.Int32 ,direction: ParameterDirection.Input);
            p.Add("Nmessage", notification.message, dbType: DbType.String, direction: ParameterDirection.Input);
            var result = _dbContext.DbConnection.Execute(
            "notification_package.CREATENOTIFICATION", p,commandType: CommandType.StoredProcedure);

        }

        public void deleteNotification(int ID)
        {
            var p = new DynamicParameters();
            p.Add("Nid", ID, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = _dbContext.DbConnection.Execute(
                "NOTIFICATION_PACKAGE.DELETENOTIFICATION", p, commandType: CommandType.StoredProcedure);
        }

        public List<Notification> getAllNotifications()
        {
             var result = _dbContext.DbConnection.Query<Notification>(
                          "notification_package.GETALLNOTIFICATIONS",commandType: CommandType.StoredProcedure );

            return result.ToList();
        }

        public  Notification getNotificationByID(int ID)
        {
            var p = new DynamicParameters();
            p.Add("Nid", ID, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = _dbContext.DbConnection.Query<Notification>(
                "notification_package.GETNOTIFICATIONBYID", p , commandType: CommandType.StoredProcedure);
            return result.FirstOrDefault();
        }

        public List<Notification> getNotificationByUserID(int IDuser)
        {
            var p = new DynamicParameters();
            p.Add("Nuserid", IDuser, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = _dbContext.DbConnection.Query<Notification>(
                "NOTIFICATION_PACKAGE.GETNOTIFICATIONBYUSERID", p, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        public void UpdateNotification(Notification notification)
        {
            var p = new DynamicParameters();
            p.Add("Nid", notification.NOTIFICATIONID, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("Nuserid", notification.userId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("Nmessage", notification.message, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("NCreatedAt", notification.createdAt, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            var result = _dbContext.DbConnection.Execute
            ("notification_package.UPDATENOTIFICATION", p,commandType:CommandType.StoredProcedure);
        }
    }
}
