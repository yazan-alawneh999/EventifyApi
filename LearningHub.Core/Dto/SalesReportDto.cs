using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningHub.Core.Dto
{
    public class SalesReportDto
    {
        public decimal eventid { get; set; }
        public string eventName { get; set; }
        public decimal TicketCount { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
