using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningHub.Core.Dto
{
    public class AttendanceDto
    {
        public Decimal EVENTID {  get; set; }
        public string EVENTNAME { get; set; }
        public decimal TOTAL_TICKETS {  get; set; }
        public decimal Attendance {  get; set; }

    }
}
