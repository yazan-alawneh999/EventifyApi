using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningHub.Core.Response;

namespace LearningHub.Core.Dto
{
    public class PinLocationEachEvent
    {

       

        // Event properties
     
        public int OrganizerID { get; set; }
        public string OrganizerName { get; set; }
        public string EventName { get; set; } = string.Empty;
        public string? EventType { get; set; }
        public DateTime EventTime { get; set; }
        public DateTime EventDate { get; set; }
        public string? EventStatus { get; set; }
        public string? Description { get; set; }
        public int? Capacity { get; set; }
        public decimal? Price { get; set; }
    


        // Navigation property for EF Core (if needed)

  
      
       
       public int LocationID { get; set; } 
       public double Latitude { get; set; }  
       public double Longitude { get; set; } 
       public int EventID { get; set; } 
      public string Address { get; set; } = string.Empty;
    }

}
