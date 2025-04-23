namespace LearningHub.Infra.Exceptions;

public class TicketNotFoundException : Exception
{
    public TicketNotFoundException() : base("Ticket not found") { }
}