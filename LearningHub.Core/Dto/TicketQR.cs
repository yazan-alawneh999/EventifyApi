using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningHub.Core.Dto
{
    public class TicketQR
    {
        public decimal ticketid { get; set; }
        public string eventname { get; set; }
        public string TicketType { get; set; }
        public string QRCode { get; set; }
        public DateTime PurchasedAt { get; set; }

    }
}
