using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using LearningHub.Core.Response;
using LearningHub.Core.Dto;

namespace LearningHub.Infra.Repository
{
    public interface IEventRepository
    {

        public List<Event> GetAllEvent();
        public Event getEventByID(int ID);

        public Task CreateEvent(CreateEventDto Event);

        public void UpdateEvent(Event Event);

        public void deleteEvent(int ID);

        Task <List<EventWithFeedBackDto>> GetAllFeedbackInEachEvent();

    }
}
