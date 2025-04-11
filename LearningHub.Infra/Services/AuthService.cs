using LearningHub.Core.Dto;
using LearningHub.Core.Repository;
using LearningHub.Core.Response;
using LearningHub.Core.Services;
using LearningHubAPI.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningHub.Infra.Services
{
    public class AuthService:IAuthService
    {
        private readonly IAuthRepo _authRepo;
        public AuthService(IAuthRepo authRepo)
        {
            _authRepo = authRepo;
        }



        public Task<LoginResponse?> Login(string username, string password)
        {
            return _authRepo.Login(username, password);
        }

        public Task<bool> UserExistsAsync(string username)
        {
            return _authRepo.UserExistsAsync(username);
        }

        public Task<bool> UserExistsAsync(decimal userId)
        {
            return _authRepo.UserExistsAsync(userId);
        }


        public Task<bool> RegisterAsync(RegisterDto registerDto)
        {
            return _authRepo.UserExistsAsync(registerDto.ToString());
        }

        public Task<List<User>> GetAllUsersAsync()
        {
            return _authRepo.GetAllUsersAsync();
        }

        public Task<User?> ValidateUserAsync(string username, string password) { 
            return _authRepo.ValidateUserAsync(username, password);
        }


    }
}
