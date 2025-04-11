

namespace LearningHub.Core.Response
{
    public class Feedback
    {

        public int FeedbackID { get; set; }
        public int UserID { get; set; }
        public int EventID { get; set; }
        public int TicketID { get; set; }
        public int Rating { get; set; }
        public DateTime FeedbackDate { get; set; }
        public string? Message { get; set; }
    }
}
