using System;
using System.Collections.Generic;

namespace LearningHub.Core.Response
{
    public class Event
    {
        public int EventID { get; set; }
        public int OrganizerID { get; set; }
        public string EventName { get; set; }
        public string? EventType { get; set; }
        public DateTime EventTime { get; set; }
        public DateTime EventDate { get; set; }
        public string EventStatus { get; set; }
        public string? Description { get; set; }
        public int? Capacity { get; set; }
        public decimal? Price { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

