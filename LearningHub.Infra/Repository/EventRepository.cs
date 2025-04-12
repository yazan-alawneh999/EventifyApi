﻿using System.Data;
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

        //public async Task<List<EventWithFeedBackDto>> GetAllFeedbackInEachEvent()
        //{
        //    var query = @"
        //SELECT e.CAPACITY, e.EVENTID, e.EVENTNAME,  
        //      f.FEEDBACKID, f.MESSAGE, f.RATING, f.USERID, f.FEEDBACKDATE,
        //      p.USERID, p.FIRSTNAME, p.LASTNAME, p.AGE, p.PROFILEIMAGE, p.PHONENUMBER 
        //FROM EVENTS e
        //INNER JOIN FEEDBACKS f ON f.EVENTID = e.EVENTID
        //INNER JOIN PROFILE p ON f.USERID = p.USERID
        //";

        //    var eventDictionary = new Dictionary<int, EventWithFeedBackDto>();

        //    var eventsWithFeedbacks = await _dbContext.DbConnection.QueryAsync<EventWithFeedBackDto, Feedback, ProfileDto, EventWithFeedBackDto>(
        //        query,
        //        (eventModel, feedback, profile) =>
        //        {

        //            Console.WriteLine($"Event Capacity: {eventModel.CAPACITY}");


        //            if (!eventDictionary.TryGetValue((int)eventModel.EVENTID, out var eventEntry))
        //            {
        //                eventEntry = new EventWithFeedBackDto
        //                {
        //                    EVENTID = eventModel.EVENTID,
        //                    EventName = eventModel.EventName,
        //                    CAPACITY = eventModel.CAPACITY,
        //                    Feedbacks = new List<Feedback>(),
        //                    ProfileDto = new List<ProfileDto>()
        //                };
        //                eventDictionary.Add((int)eventModel.EVENTID, eventEntry);
        //            }


        //            eventEntry.Feedbacks.Add(feedback);
        //            eventEntry.ProfileDto.Add(profile);

        //            return eventEntry;
        //        },
        //                        splitOn: "FEEDBACKID,USERID"
        //                    );


        //    return eventDictionary.Values.ToList();
        //}




    }
}
