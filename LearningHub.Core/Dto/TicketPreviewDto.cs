using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningHub.Core.Dto
{
    public class TicketPreviewDto
    {
        public decimal ticketid {  get; set; }
        public string tickettype { get; set; }
        public string eventname { get; set; }
        public DateTime purchasedat {  get; set; }
    }
}
