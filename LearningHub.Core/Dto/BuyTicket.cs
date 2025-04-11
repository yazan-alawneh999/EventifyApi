using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningHub.Core.Dto
{
    public class BuyTicket
    {
        public decimal t_EventID {  get; set; }
        public decimal t_UserID { get;  set; }
        public string t_TicketType { get; set; }
        public decimal t_Price { get; set; }
        public string t_Discount {  get; set; }
        public int Returned_Status { get; set; } // todo:remove 

    }
}
