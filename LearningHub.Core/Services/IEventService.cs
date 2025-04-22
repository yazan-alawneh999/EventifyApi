using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningHub.Core.Dto;
using LearningHub.Core.Response;

namespace LearningHub.Core.Services
{
    public interface IEventService
    {

        public List<Event> GetAllEvent();
        public Event getEventByID(int ID);

        public Task CreateEvent(CreateEventDto Event);

        public void UpdateEvent(Event Event);

        public void deleteEvent(int ID);
        Task<List<EventWithFeedBackDto>> GetAllFeedbackInEachEvent();

    }
}
