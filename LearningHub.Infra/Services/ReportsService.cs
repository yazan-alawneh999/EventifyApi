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
    public class ReportsService:IReportsService
    {
        private readonly IReportsRepository _reportsRepository;

        public ReportsService(IReportsRepository reportsRepository)
        {
            _reportsRepository = reportsRepository;
        }

        public List<SalesReportDto> GetSalesRrport()
        {
            return _reportsRepository.GetSalesRrport();
        }
        public AttendanceDto GetAttendanceReport(int id)
        {
            return _reportsRepository.GetAttendanceReport(id);
        }

        public List<TicketInfo> GetAttendaseList(decimal id)
        { 
            return _reportsRepository.GetAttendaseList(id);
        }
    }
}
