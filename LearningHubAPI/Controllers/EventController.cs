using System.Security.Claims;
using LearningHub.Core.Dto;
using LearningHub.Core.Response;
using LearningHub.Core.Services;
using LearningHubAPI.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearningHubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService eventService;

        public EventController(IEventService _eventService)
        {
            this.eventService = _eventService;
        }



        [HttpGet]
        [Route("GetAllEvent")]
        [Authorize]
        [IdentityRequiresClaims(ClaimTypes.Role, new[] { "1", "2", "3" })] // 1 admin, 2 org, 3 user

        public List<Event> GetAllEvent() 
        {
           return eventService.GetAllEvent();
        }
        [HttpGet]
        [Route("getEventByID/{ID}")]
        [IdentityRequiresClaims(ClaimTypes.Role, new[] { "1", "2", "3" })] // 1 admin, 2 org, 3 user

        public Event getEventByID(int ID) 
        {
            return eventService.getEventByID(ID);
        }

        [HttpPost]
        [Route("CreateEvent")]
        [IdentityRequiresClaims(ClaimTypes.Role, new[] { "1", "2" })] // 1 admin, 2 org, 3 user

        public void CreateEvent(Event Event) 
        {
            eventService.CreateEvent(Event);
        }

        [IdentityRequiresClaims(ClaimTypes.Role, new[] { "1", "2"})] // 1 admin, 2 org, 3 user

        [HttpPut]
        [Route("UpdateEvent")]
        public void UpdateEvent(Event Event)
        {
            eventService.UpdateEvent(Event);
        }

     
        [HttpDelete]
        [Route("DeleteEventByID/{ID}")]
        [IdentityRequiresClaims(ClaimTypes.Role, new[] { "1", "2"})] // 1 admin, 2 org, 3 user

        public void deleteEvent(int ID)
        {
            eventService.deleteEvent(ID);   
        }


       [HttpGet]
[Route("GetAllFeedbackInEachEvent")]
        [IdentityRequiresClaims(ClaimTypes.Role, new[] { "1", "2", "3" })] // 1 admin, 2 org, 3 user

        public async Task<IActionResult> GetAllFeedbackInEachEvent()
{
    try
    {
        var eventsWithFeedbacks = await eventService.GetAllFeedbackInEachEvent();

        if (eventsWithFeedbacks == null || !eventsWithFeedbacks.Any())
            return NotFound("No feedback found for any event.");

        return Ok(eventsWithFeedbacks);
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Internal Server Error: {ex.Message}");
    }
}


    }
}
