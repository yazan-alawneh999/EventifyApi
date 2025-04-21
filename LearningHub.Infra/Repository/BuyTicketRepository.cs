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
    public class BuyTicketRepository:IBuyTicketRepository
    {
        private readonly IDbContext _dbContext;
        

        public BuyTicketRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool BuyTicket(BuyTicket TicketInfo,String qrText)
        {
            var p = new DynamicParameters();
            p.Add("t_EventID", TicketInfo.t_EventID, dbType: DbType.Int32);
            p.Add("t_UserID", TicketInfo.t_UserID, dbType: DbType.Int32);
            p.Add("t_TicketType", TicketInfo.t_TicketType, dbType: DbType.String);
            p.Add("t_Price", TicketInfo.t_Price, dbType: DbType.Decimal);
            p.Add("t_QRCode", qrText, dbType: DbType.String);
            p.Add("t_Discount", TicketInfo.t_Discount, dbType: DbType.String);

            try {
                _dbContext.DbConnection.Execute("Tickets_Package.BuyTicket", p, commandType: CommandType.StoredProcedure);
                return true;
            }
            catch { 
                return false;
            }

        }

        public List<TicketPreviewDto> GetAllTicketsByUserId(decimal userID) {
            var p = new DynamicParameters();
            p.Add("t_UserID", userID, dbType: DbType.Int32);
            var result= _dbContext.DbConnection.Query<TicketPreviewDto>("Tickets_Package.GetAllTicketsByUserId", p, commandType: CommandType.StoredProcedure).ToList();
            return result;
        }

        public async Task<string> CheckInByQRCodeAsync(string qrCode)
        {
            var ticket = await GetTicketByQRCodeAsync(qrCode);
            if (ticket == null)
                return "Ticket not found";

            // Optionally check if already checked in
            var alreadyCheckedIn = await IsTicketAlreadyCheckedIn(ticket.TicketID);
            if (alreadyCheckedIn)
                return "Ticket already checked in";

            await CreateCheckInAsync(ticket.TicketID);
            return "Check-in successful";
        }
        public async Task<BuyTicket?> GetTicketByQRCodeAsync(string qrCode)
        {
            var sql = "SELECT * FROM Tickets WHERE QRCode = :QRCode";
            await using var connection = _dbContext.DbConnection; // OracleConnection for Oracle DB
            return await connection.QueryFirstOrDefaultAsync<BuyTicket>(sql, new { QRCode = qrCode });
        }
        
        public async Task<int> CreateCheckInAsync(int ticketId, string status = "Checked In")
        {
            var sql = @"
        INSERT INTO CheckIns (TicketID, CheckInTime, Status)
        VALUES (:TicketID, SYSDATE, :Status)";
    
            await using var connection = _dbContext.DbConnection;
            return await connection.ExecuteAsync(sql, new { TicketID = ticketId, Status = status });
        }
        
        private async Task<bool> IsTicketAlreadyCheckedIn(int ticketId)
        {
            var sql = "SELECT COUNT(1) FROM CheckIns WHERE TicketID = :TicketID";
            await using var connection = _dbContext.DbConnection;
            var count = await connection.ExecuteScalarAsync<int>(sql, new { TicketID = ticketId });
            return count > 0;
        }

    }
  

}
