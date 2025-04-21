using LearningHub.Core.Dto;
using LearningHub.Core.Repository;
using LearningHub.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningHub.Infra.Services
{
    public class BuyTicketService:IBuyTicketService
    {
        private readonly IBuyTicketRepository _buyTicketRepository;
        private readonly QRCodeService _qrCodeService;
        public BuyTicketService(IBuyTicketRepository buyTicketRepository, QRCodeService qrCodeService)
        {
            _buyTicketRepository = buyTicketRepository;
            _qrCodeService = qrCodeService;
        }

        public bool BuyTicket(BuyTicket TicketInfo)
        {

            string code = _qrCodeService.GenerateQRCodeText(TicketInfo.t_EventID, TicketInfo.t_UserID);
            return _buyTicketRepository.BuyTicket(TicketInfo,code );
        }

        public List<TicketPreviewDto> GetAllTicketsByUserId(decimal userID)
        {
            return _buyTicketRepository.GetAllTicketsByUserId(userID);
        }

        public TicketQR GetTicketsByTicketId(decimal ticketID) {
            return _buyTicketRepository.GetTicketsByTicketId(ticketID);  
        }
    }

}
