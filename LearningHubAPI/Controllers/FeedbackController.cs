using System.Security.Claims;
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
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService feedbackService;
        public FeedbackController(IFeedbackService feedbackService)
        {
            this.feedbackService = feedbackService;
        }

        [Authorize]
        [HttpGet]
        [Route("getAllFeedback")]
        [IdentityRequiresClaims(ClaimTypes.Role, new[] { "1", "2", "3" })] // 1 admin, 2 org, 3 user

        public List<Feedback> getAllFeedbacks()
        {
          return  feedbackService.getAllFeedbacks();
        }
        [HttpGet]
        [Route("getFeedbackByID/{ID}")]
        [IdentityRequiresClaims(ClaimTypes.Role, new[] { "1", "2", "3" })] // 1 admin, 2 org, 3 user

        public Feedback getFeedbackByID(int ID) {
          return  feedbackService.getFeedbackByID(ID);        
        }
        [HttpPost]
        [Route("CreateFeedback")]
        [IdentityRequiresClaims(ClaimTypes.Role, new[] { "1", "2", "3" })] // 1 admin, 2 org, 3 user

        public void CreateFeedback(Feedback feedback) 
        {
            feedbackService.CreateFeedback(feedback);
        }
        [HttpPut]
        [Route("UpdateFeedback")]
        [IdentityRequiresClaims(ClaimTypes.Role, new[] { "1", "2", "3" })] // 1 admin, 2 org, 3 user

        public void UpdateFeedback(Feedback feedback)
        {
            feedbackService.UpdateFeedback(feedback);
        }
        [HttpDelete]
        [Route("deleteFeedback/{ID}")]
        [IdentityRequiresClaims(ClaimTypes.Role, new[] { "1", "2", "3" })] // 1 admin, 2 org, 3 user

        public void deleteFeedback(int ID) 
        {
            feedbackService.deleteFeedback(ID);
        }

    }
}
