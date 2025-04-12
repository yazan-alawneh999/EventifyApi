using LearningHub.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningHub.Core.Services
{
    public interface IReportsService
    {
        List<SalesReportDto> GetSalesRrport();
        AttendanceDto GetAttendanceReport(int id);
        List<TicketInfo> GetAttendaseList(decimal id);

    }
}
