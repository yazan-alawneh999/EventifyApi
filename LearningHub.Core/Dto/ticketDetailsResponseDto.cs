using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningHub.Core.Dto
{
    class ticketDetailsResponseDto
    {
        public decimal t_EventID { get; set; }
        public decimal t_UserID { get; set; }
        public string t_TicketType { get; set; }
        public decimal t_Price { get; set; }
        public string t_Discount { get; set; }
     
        public string QRCode { get; set; }
    }
}
