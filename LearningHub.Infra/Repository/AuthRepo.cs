using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Dapper;
using LearningHub.Core.Common;
using LearningHub.Core.Dto;

using LearningHub.Core.Repository;
using LearningHub.Core.Response;
using LearningHub.Infra.Util;
using LearningHubAPI.Requests;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Oracle.ManagedDataAccess.Client;

namespace LearningHub.Infra.Repository
{
    
    
    public class AuthRepo : IAuthRepo
    {
      
        private readonly IConfiguration _configuration;
        private readonly IDbContext _dbContext; // Use DbContext here
        private  UtilService _utilService;

        public AuthRepo(UtilService util, IConfiguration configuration, IDbContext dbContext)
        {
        
            _configuration = configuration;
            _dbContext = dbContext; // Inject and assign DbContext
            _utilService = util;
        }

        public async Task<LoginResponse?> Login(string username, string password)
        {
            // Validate user credentials using UserRepository
            var user = await ValidateUserAsync(username, password);

            if (user is null)
            {
                return null;
            }
            var jwtToken =  GenerateJwtToken(user.Username, user.Roleid);
            var loginResponse = new LoginResponse()
            {
                token = jwtToken,
                userId = user.Userid
            };
            // Return null or throw an exception if the user is invalid
            return  loginResponse;
        }
        public async Task<bool> UserExistsAsync(string username)
        {
            const string query = @"SELECT COUNT(1) FROM Users WHERE Username = :Username";

            var parameters = new { Username = username };

            await using var connection = _dbContext.DbConnection;
    
            // Correct method to return actual count
            var count = await connection.ExecuteScalarAsync<int>(query, parameters); 

            return count > 0; 
        }

        public async Task<bool> UserExistsAsync(decimal userId)
        {
            const string query = @"SELECT COUNT(1) FROM Users WHERE UserID = :UserId";

            var parameters = new { UserId = userId };

            await using var connection = _dbContext.DbConnection;
    
            // Correct method to return actual count
            var count = await connection.ExecuteScalarAsync<int>(query, parameters); 

            return count > 0; 
        }

        public async Task<bool> RegisterAsync(RegisterDto registerDto)
        {
          
            await using var connection = _dbContext.DbConnection;
            var query = @"INSERT INTO Users (Username, PasswordHash, LastLogin, RoleID) 
                  VALUES (:Username, :PasswordHash, SYSDATE, :RoleID)";  // Using ":" for Oracle parameters

            var parameters = new
            {
                Username = registerDto.Username,
                PasswordHash = registerDto.Password,  
                RoleID = registerDto.RoleID
            };

            var result = await connection.ExecuteAsync(query, parameters);
            return result > 0;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            await using var connection = _dbContext.DbConnection;

            var users = await connection.QueryAsync(
                @"SELECT 
                            u.UserID, u.Username, u.PasswordHash,u.LastLogin as CreateAt,
                            p.ProfileID, p.ProfileImage,  
                            r.RoleID AS RoleId, r.RoleName     
                            FROM Users u
                            LEFT JOIN Roles r ON u.RoleID = r.RoleID
                            LEFT JOIN Profile p ON u.UserID = p.UserID",
                
                (User user, RoleDto role) =>
                {
                    user.ProfileImage = _utilService.GetProfileImageUrl(user.ProfileImage);
                    user.Role = role; 
                    return user;
                },
                splitOn: "RoleID" 
            );

            return users.ToList();
           
        }

        public async Task<User> ValidateUserAsync(string username, string password)
        {
            await using var connection = _dbContext.DbConnection;
            
            try
            {
                // Open the connection
                await connection.OpenAsync();

                // Define the parameters
                var parameters = new DynamicParameters();
                parameters.Add("User_NAME", username, DbType.String, ParameterDirection.Input);
                parameters.Add("PASS", password, DbType.String, ParameterDirection.Input);
            

                // Query using the User_Login procedure
                var result = await connection.QueryAsync<User>(
                    "Login_Package.User_Login", 
                    parameters, 
                    commandType: CommandType.StoredProcedure);


           
                // If result is empty, return null (invalid username/password)
                var user = result.FirstOrDefault();

                // Return the tuple with username and role
            
                return user;
            }
            catch (Exception ex)
            {
                return null; 
            }
            finally
            {
                // Cleanup
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var sql = "DELETE FROM Users WHERE UserID = :UserId";

            await using var connection = _dbContext.DbConnection;
            var rowsAffected = await connection.ExecuteAsync(sql, new { UserId = userId });
            return rowsAffected > 0;
        }


        private string? GenerateJwtToken(string username, decimal roleId)
        {
            
          
            // Key from appsettings.json
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Token claims
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, roleId.ToString())
            };

            // Define token
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],      // Issuer
                _configuration["Jwt:Audience"],   // Audience 
                claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials);

            // Return token as a string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        
    }
   
}