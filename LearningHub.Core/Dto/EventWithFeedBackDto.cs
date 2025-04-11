
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LearningHub.Core.Dto
{
    public class EventWithFeedBackDto
    {
       
    public decimal EVENTID { get; set; }
    public string EventName { get; set; }
    public decimal? ERating { get; set; }
    public double CAPACITY { get; set; }
        public List<FeedbackDto> ParsedFeedbackDetails
        {
            get
            {
                return string.IsNullOrEmpty(FeedbackDetails)
                ? new List<FeedbackDto>()
                    : JsonConvert.DeserializeObject<List<FeedbackDto>>(FeedbackDetails);
            }
        }
        [Newtonsoft.Json.JsonIgnore]
        public string FeedbackDetails { get; set; }

        


    }

    
    
}
public class ProfilesDtoFeedback
{
    public int UserId { get; set; }         
    public string FirstName { get; set; }   
    public string LastName { get; set; }    
    public int Age { get; set; }            
    public string ProfileImage { get; set; } 
    public string PhoneNumber { get; set; }
}

public class FeedbackDto
{
    public int FeedbackId { get; set; }      
    public string Message { get; set; }      
    public double Rating { get; set; }       
    public DateTime FeedbackDate { get; set; }

    
    public ProfilesDtoFeedback User { get; set; }
}