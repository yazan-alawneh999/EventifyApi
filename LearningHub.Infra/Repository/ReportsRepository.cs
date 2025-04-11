using Dapper;
using LearningHub.Core.Common;
using LearningHub.Core.Dto;
using LearningHub.Core.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningHub.Infra.Repository
{
    public class ReportsRepository:IReportsRepository
    {
        private readonly IDbContext _dbContext;

        public ReportsRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

       public List<SalesReportDto> GetSalesRrport()
        {
            
            IEnumerable<SalesReportDto> result = _dbContext.DbConnection.Query<SalesReportDto>("REPORTS.GetTicketSalesReport", commandType: CommandType.StoredProcedure).ToList();
            return (List<SalesReportDto>)result;
        }

        public AttendanceDto GetAttendanceReport(int id)
        {
            var p = new DynamicParameters();
            p.Add("EvtID", id,dbType:DbType.Int32);
            var result = _dbContext.DbConnection.Query<AttendanceDto>("REPORTS.AttendanceReports", p, commandType: CommandType.StoredProcedure).SingleOrDefault();
            return result;
        }

    }
}
