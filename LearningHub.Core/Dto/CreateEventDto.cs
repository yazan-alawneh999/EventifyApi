namespace LearningHub.Core.Dto;

public class CreateEventDto
{
    public int OrganizerID { get; set; }
    public string EventName { get; set; } = string.Empty;
    public string? EventType { get; set; }
    public DateTime EventTime { get; set; }
    public DateTime EventDate { get; set; }
    public string? EventStatus { get; set; }
    public string? Description { get; set; }
    public int? Capacity { get; set; }
    public decimal? Price { get; set; }
    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    // Location properties (embedded)
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Address { get; set; } = string.Empty;
}