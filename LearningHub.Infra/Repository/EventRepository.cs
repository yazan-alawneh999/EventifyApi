using System.Data;
using System.Diagnostics;
using Dapper;
using LearningHub.Core.Common;
using LearningHub.Core.Dto;
using LearningHub.Core.Response;
using Microsoft.Extensions.Configuration;

namespace LearningHub.Infra.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly IDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public EventRepository(IDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }
        public void CreateEvent(Event Event)
        {
            var p = new DynamicParameters();
          
            p.Add("Organizer_id", Event.OrganizerID, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("event_Name", Event.EventName, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("Event_Type", Event.EventType, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("Event_Time", Event.EventTime, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            p.Add("event_Date", Event.EventDate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            p.Add("event_Status", Event.EventStatus, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("E_description", Event.Description, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("E_capacity", Event.Capacity, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("E_price", Event.Price, dbType: DbType.Decimal, direction: ParameterDirection.Input);
            
            var result = _dbContext.DbConnection.Execute(
                "event_package.createEvent", p, commandType: CommandType.StoredProcedure);

        }

        public void deleteEvent(int ID)
        {
            var p = new DynamicParameters();
            p.Add("Eid", ID, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = _dbContext.DbConnection.Execute(
                "event_package.deleteEvent", p, commandType: CommandType.StoredProcedure);
        }

        public List<Event> GetAllEvent()
        {
           IEnumerable<Event> result = _dbContext.DbConnection.Query<Event>(
                "event_package.getAllEvent", commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        public async Task<List<EventWithFeedBackDto>> GetAllFeedbackInEachEvent()
        {
            IEnumerable<EventWithFeedBackDto> result = _dbContext.DbConnection.Query<EventWithFeedBackDto>
                ("event_package.getAllEventWithFeedback", commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        public Event getEventByID(int ID)
        {
            var p = new DynamicParameters();
            p.Add("Eid",ID, dbType: DbType.Int32, direction: ParameterDirection.Input);

            var result = _dbContext.DbConnection.Query<Event>(
               "event_package.getEventByID", p, commandType: CommandType.StoredProcedure);
            return result.FirstOrDefault();
        }

        public void UpdateEvent(Event Event)
        {
            var p = new DynamicParameters() ;
            p.Add("Eid", Event.EventID, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("Organizer_id", Event.OrganizerID, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("event_Name", Event.EventName, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("Event_Type", Event.EventType, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("Event_Time", Event.EventTime, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            p.Add("event_Date", Event.EventDate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            p.Add("event_Status", Event.EventStatus, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("E_description", Event.Description, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("E_capacity", Event.Capacity, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("E_price", Event.Price, dbType: DbType.Decimal, direction: ParameterDirection.Input);

            var result = _dbContext.DbConnection.Execute(
                "event_package.UpdateEvent", p, commandType: CommandType.StoredProcedure);
        }

        

    }
}
