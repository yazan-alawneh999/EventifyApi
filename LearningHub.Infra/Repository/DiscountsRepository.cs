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
    public class DiscountsRepository: IDiscountsRepository
    {

        private readonly IDbContext _dbContext;
        public DiscountsRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<DiscountDto> GetDiscountsByUserID(decimal userID)
        {
            var p = new DynamicParameters();
            p.Add("p_UserID", userID, dbType: DbType.Int32);
            var result = _dbContext.DbConnection.Query<DiscountDto>("Discount_Package.GetDiscountsByUserID",p, commandType: CommandType.StoredProcedure).ToList();
            return result;
        }

        public DiscountDto GetDiscountsByUserAndCode(decimal userID, String Code) {
            var p = new DynamicParameters();
            p.Add("p_UserID", userID, dbType: DbType.Int32);
            p.Add("p_DiscountCode", Code, dbType: DbType.String);
            var result = _dbContext.DbConnection.Query<DiscountDto>("Discount_Package.GetDiscountsByUserAndCode", p, commandType: CommandType.StoredProcedure).SingleOrDefault();
            return result;
        }
    }
}
