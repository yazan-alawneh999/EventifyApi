using LearningHub.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningHub.Core.Repository
{
    public interface IReportsRepository
    {

        List<SalesReportDto> GetSalesRrport();
        AttendanceDto GetAttendanceReport(int id);


    }
}
