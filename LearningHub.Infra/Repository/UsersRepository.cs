using LearningHub.Core.Common;
using LearningHub.Core.Repository;
using LearningHub.Core.Response;
using Microsoft.Extensions.Configuration;

namespace LearningHub.Infra.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public UsersRepository(IDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }
        public void CreateUser(Users user)
        {
            throw new NotImplementedException();
        }

        public void deleteUser(int ID)
        {
            throw new NotImplementedException();
        }

        public List<Users> getAllUsers()
        {
            throw new NotImplementedException();
        }

        public Users getUserByID(int ID)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(Users user)
        {
            throw new NotImplementedException();
        }
    }
}
