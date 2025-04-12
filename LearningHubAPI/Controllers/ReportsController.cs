using LearningHub.Core.Dto;
using LearningHub.Core.Services;
using LearningHub.Infra.Services;
using LearningHubAPI.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LearningHubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {

        private readonly IReportsService _reportsService;
        public ReportsController(IReportsService reportsService)
        {
            _reportsService = reportsService;
        }

        [HttpGet]
        [Route("GetSalesReport")]
        [Authorize]
        [IdentityRequiresClaims(ClaimTypes.Role, new[] { "1", "2" })]
        public List<SalesReportDto> GetSalesRrport()
        {
            return _reportsService.GetSalesRrport();
        }


        [HttpGet]
        [Route("GetAttendanceReport/{id}")]
        [Authorize]
        [IdentityRequiresClaims(ClaimTypes.Role, new[] { "1","2" })]
        public AttendanceDto GetAttendanceReport(int id)
        {
            return _reportsService.GetAttendanceReport(id);
        }

        [HttpGet]
        [Route("GetAttendanceList/{id}")]
        [Authorize]
        [IdentityRequiresClaims(ClaimTypes.Role, new[] { "1", "2" })]
        public List<TicketInfo> GetAttendaseList(decimal id) {
            return _reportsService.GetAttendaseList(id);
        }
    }
}
