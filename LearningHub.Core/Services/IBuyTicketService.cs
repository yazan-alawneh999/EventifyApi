using LearningHub.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningHub.Core.Services
{
    public interface IBuyTicketService
    {
        public bool BuyTicket(BuyTicket TicketInfo);
        public List<TicketPreviewDto> GetAllTicketsByUserId(decimal userID);
        public Task<string> CheckInByQRCodeAsync(string qrCode);
        public TicketQR GetTicketsByTicketId(decimal ticketID);


    }
}
