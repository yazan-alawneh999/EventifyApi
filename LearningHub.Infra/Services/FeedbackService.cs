using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningHub.Core.Repository;
using LearningHub.Core.Response;
using LearningHub.Core.Services;

namespace LearningHub.Infra.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public FeedbackService(IFeedbackRepository feedbackRepository) 
        {
            _feedbackRepository = feedbackRepository;
        }
        public void CreateFeedback(Feedback feedback)
        {
            _feedbackRepository.CreateFeedback(feedback);
        }

        public void deleteFeedback(int ID)
        {
            _feedbackRepository.deleteFeedback(ID);
        }

        public List<Feedback> getAllFeedbacks()
        {
            return _feedbackRepository.getAllFeedbacks();
        }

        public Feedback getFeedbackByID(int ID)
        {
           return _feedbackRepository.getFeedbackByID(ID);
        }

        public void UpdateFeedback(Feedback feedback)
        {
            _feedbackRepository.UpdateFeedback(feedback);   
        }
    }

}
