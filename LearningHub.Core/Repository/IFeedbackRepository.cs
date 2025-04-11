using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningHub.Core.Response;

namespace LearningHub.Core.Repository
{
    public interface IFeedbackRepository
    {

        public List<Feedback> getAllFeedbacks();
        public Feedback getFeedbackByID(int ID);
        public void CreateFeedback(Feedback feedback);
        public void UpdateFeedback(Feedback feedback);
        public void deleteFeedback(int ID);

    }
}
