namespace LearningHub.Infra.Exceptions;

public class TicketAlreadyCheckedInException : Exception
{
    public TicketAlreadyCheckedInException() : base("Ticket already checked in") { }
}