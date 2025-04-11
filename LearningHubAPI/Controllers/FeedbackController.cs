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
        [IdentityRequiresClaims(ClaimTypes.Role, new[] { "1" })]
        [HttpGet]
        [Route("getAllFeedback")]
        public List<Feedback> getAllFeedbacks()
        {
          return  feedbackService.getAllFeedbacks();
        }
        [HttpGet]
        [Route("getFeedbackByID/{ID}")]
        public Feedback getFeedbackByID(int ID) {
          return  feedbackService.getFeedbackByID(ID);        
        }
        [HttpPost]
        [Route("CreateFeedback")]
        public void CreateFeedback(Feedback feedback) 
        {
            feedbackService.CreateFeedback(feedback);
        }
        [HttpPut]
        [Route("UpdateFeedback")]
        public void UpdateFeedback(Feedback feedback)
        {
            feedbackService.UpdateFeedback(feedback);
        }
        [HttpDelete]
        [Route("deleteFeedback/{ID}")]
        public void deleteFeedback(int ID) 
        {
            feedbackService.deleteFeedback(ID);
        }

    }
}
