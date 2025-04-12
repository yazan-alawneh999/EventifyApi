using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningHub.Core.Dto
{
    public class TicketInfo
    {
        public decimal UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TicketType { get; set; }
        public string QRCode { get; set; }
        public DateTime PurchasedAt { get; set; }

    }
}
