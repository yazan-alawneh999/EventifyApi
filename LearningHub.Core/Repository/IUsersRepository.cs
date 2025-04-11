using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningHub.Core.Response;

namespace LearningHub.Core.Repository
{
    public interface IUsersRepository
    {
        public List<Users> getAllUsers();
        public Users getUserByID(int ID);
        public void CreateUser(Users user);
        public void UpdateUser(Users user);
        public void deleteUser(int ID);
    }
}
