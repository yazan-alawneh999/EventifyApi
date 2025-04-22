namespace LearningHub.Core.Dto;

public class TicketModel
{
    public int TicketID { get; set; }
    public int EventID { get; set; }
    public int UserID { get; set; }
    public string TicketType { get; set; }
    public string QRCode { get; set; }
    public int? SalesID { get; set; }
    public DateTime PurchasedAt { get; set; }
}