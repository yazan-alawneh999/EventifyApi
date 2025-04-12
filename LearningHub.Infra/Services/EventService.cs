
using LearningHub.Core.Dto;
using LearningHub.Core.Response;
using LearningHub.Core.Services;
using LearningHub.Infra.Repository;

namespace LearningHub.Infra.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService (IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public void CreateEvent(Event Event)
        {
            _eventRepository.CreateEvent(Event);
        }

        public void deleteEvent(int ID)
        {
            _eventRepository.deleteEvent(ID);
        }

        public List<Event> GetAllEvent()
        {
            return _eventRepository.GetAllEvent();
        }

        public Task<List<EventWithFeedBackDto>> GetAllFeedbackInEachEvent()
        {
            return _eventRepository.GetAllFeedbackInEachEvent();
        }

        public Event getEventByID(int ID)
        {
            return _eventRepository.getEventByID(ID);
        }

        public void UpdateEvent(Event Event)
        {
            _eventRepository.UpdateEvent(Event);    
        }
    }
}
