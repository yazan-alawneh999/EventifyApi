
using System.Data;
using Dapper;
using LearningHub.Core.Common;
using LearningHub.Core.Repository;
using LearningHub.Core.Response;
using Microsoft.Extensions.Configuration;

namespace LearningHub.Infra.Repository
{
    public class FeedbackRepositorycs : IFeedbackRepository
    {


        private readonly IDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public FeedbackRepositorycs(IDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }
        public void CreateFeedback(Feedback feedback)
        {
            var p = new DynamicParameters();
            p.Add("User_ID", feedback.UserID, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("Event_ID", feedback.EventID, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("Ticket_ID", feedback.TicketID, dbType: DbType.Int32, direction: ParameterDirection.Input); // ✅ تعديل هنا
            p.Add("Rating", feedback.Rating, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("Message", feedback.Message, dbType: DbType.String, direction: ParameterDirection.Input);

            var result = _dbContext.DbConnection.Execute(
                "FEEDBACKS_PACKAGE.CREATEFEEDBACK", p, commandType: CommandType.StoredProcedure);
        }


        public void deleteFeedback(int ID)
        {
            var p = new DynamicParameters();
            p.Add("Feedback_ID", ID, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = _dbContext.DbConnection.Execute(
                "FEEDBACKS_PACKAGE.deleteFeedback", p, commandType: CommandType.StoredProcedure);


        }

        public List<Feedback> getAllFeedbacks()
        {
            IEnumerable<Feedback> result = _dbContext.DbConnection.Query<Feedback>(
                "FEEDBACKS_PACKAGE.getAllFeedbacks", commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        public Feedback getFeedbackByID(int ID)
        {
           var p = new DynamicParameters();
            p.Add("Feedback_ID", ID, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = _dbContext.DbConnection.Query<Feedback>(
                "FEEDBACKS_PACKAGE.GETFEEDBACKBYID", p, commandType: CommandType.StoredProcedure);
            return result.FirstOrDefault();
        }


        public void UpdateFeedback(Feedback feedback)
        {
            var p = new DynamicParameters();
            p.Add("Feedback_ID", feedback.FeedbackID, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("User_ID", feedback.UserID, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("Event_ID", feedback.EventID, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("Ticket_ID", feedback.TicketID, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("Rating", feedback.Rating, dbType: DbType.Int32, direction: ParameterDirection.Input);

          
            p.Add("Feedback_Date", feedback.FeedbackDate, dbType: DbType.DateTime, direction: ParameterDirection.Input);

            p.Add("Message", feedback.Message, dbType: DbType.String, direction: ParameterDirection.Input);


            var result = _dbContext.DbConnection.Execute(
                "FEEDBACKS_PACKAGE.UPDATEFEEDBACK", p, commandType: CommandType.StoredProcedure);
        }
    }
}
