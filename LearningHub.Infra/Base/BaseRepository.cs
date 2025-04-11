using System.Data;
using LearningHub.Core.Common;

namespace LearningHub.Infra.Base;


    public abstract class BaseRepository
    {
        protected readonly IDbContext _dbContext;

        protected BaseRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected IDbConnection Connection => _dbContext.DbConnection; 
    
}