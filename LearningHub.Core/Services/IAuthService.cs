using LearningHub.Core.Dto;
using LearningHub.Core.Response;
using LearningHubAPI.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningHub.Core.Services
{
    public interface IAuthService
    {

        Task<LoginResponse?> Login(string username, string password);
        Task<bool> UserExistsAsync(string username); // Check for duplicates
        Task<bool> UserExistsAsync(decimal userId); // Check for duplicates
        Task<bool> RegisterAsync(RegisterDto registerDto); // Insert user

        Task<List<User>> GetAllUsersAsync();
        Task<User?> ValidateUserAsync(string username, string password);
    }
}
