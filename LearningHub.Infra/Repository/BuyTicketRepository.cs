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

        public int BuyTicket(BuyTicket TicketInfo,String qrText)
        {


         
            var p = new DynamicParameters();
            p.Add("t_EventID", TicketInfo.t_EventID, dbType: DbType.Int32);
            p.Add("t_UserID", TicketInfo.t_UserID, dbType: DbType.Int32);
            p.Add("t_TicketType", TicketInfo.t_TicketType, dbType: DbType.String);
            p.Add("t_Price", TicketInfo.t_Price, dbType: DbType.Decimal);
            p.Add("t_QRCode", qrText, dbType: DbType.String);
            p.Add("t_Discount", TicketInfo.t_Discount, dbType: DbType.String);
            p.Add("Returned_Status", TicketInfo.Returned_Status, dbType: DbType.Int32, direction: ParameterDirection.Output);

            _dbContext.DbConnection.Execute("Tickets_Package.BuyTicket", p, commandType: CommandType.StoredProcedure);
            int status = p.Get<int>("Returned_Status");

            return status; // status > 0 ;  

        }
    }

}
