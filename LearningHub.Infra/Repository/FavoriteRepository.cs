using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using LearningHub.Core.Common;
using LearningHub.Core.Response;
using Microsoft.Extensions.Configuration;

namespace LearningHub.Infra.Repository
{
    public class FavoriteRepository : IFavoriteRepository
    {

        private readonly IDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public FavoriteRepository(IDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }
        public void CreateFavorite(Favorite favorite)
        {
            var p = new DynamicParameters();    
            p.Add("Event_ID", favorite.EventID, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("User_ID", favorite.UserID, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = _dbContext.DbConnection.Execute(
                "FAVORITES_PACKAGE.CREATEFAVORITE", p, commandType: CommandType.StoredProcedure);
        }

        public void deleteFavorite(int ID)
        {
            var p = new DynamicParameters();
            p.Add("Fav_ID", ID, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = _dbContext.DbConnection.Execute(
                "FAVORITES_PACKAGE.DELETEFAVORITE", p, commandType: CommandType.StoredProcedure);
        }

        public List<Favorite> getAllFavorites()
        {
            IEnumerable<Favorite> result = _dbContext.DbConnection.Query<Favorite>(
                "Favorites_package.GETALLFAVORITES", commandType:CommandType.StoredProcedure);
            return result.ToList();
        }


        public Favorite getFavoriteByID(int ID)
        {
            var p = new DynamicParameters();
             p.Add("Fav_ID", ID, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = _dbContext.DbConnection.Query<Favorite>(
                "FAVORITES_PACKAGE.GETFAVORITEBYID", p, commandType: CommandType.StoredProcedure);
            return result.FirstOrDefault();
        }

        public void UpdateFavorite(Favorite favorite)
        {
            var p = new DynamicParameters();
            p.Add("Fav_ID", favorite.FavoriteID, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("Event_ID", favorite.EventID, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("User_ID", favorite.UserID, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = _dbContext.DbConnection.Execute(
                "FAVORITES_PACKAGE.UPDATEFAVORITE", p, commandType: CommandType.StoredProcedure);    
        }
    }
}
