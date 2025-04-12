using LearningHub.Core.Dto;

using LearningHub.Core.Response;

using LearningHubAPI.Requests;

namespace LearningHub.Core.Repository;

public interface IAuthRepo
{
    
    Task<LoginResponse?> Login(string username, string password);
    Task<bool> UserExistsAsync(string username); // Check for duplicates
    Task<bool> UserExistsAsync(decimal userId); // Check for duplicates
    Task<bool> RegisterAsync(RegisterDto registerDto); // Insert user
    
    Task<List<User>> GetAllUsersAsync();
    Task<User> ValidateUserAsync(string username, string password);
    

  
}