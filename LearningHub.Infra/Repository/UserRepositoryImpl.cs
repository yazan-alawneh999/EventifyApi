using System.Data;
using Dapper;
using LearningHub.Core.Common;
using LearningHub.Core.Dto;
using LearningHub.Core.Repository;

using LearningHub.Infra.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Oracle.ManagedDataAccess.Client;

namespace LearningHub.Infra.Repository;

public class UserRepositoryImpl:BaseRepository,IUserRepository
{

   

    public UserRepositoryImpl(IDbContext dbContext , IConfiguration configuration):base(dbContext)
        
    {

    }

    public  async Task<bool> UpdateUserAsync(decimal userId, UpdateUserProfileDto dto)
    {
        await using var connection = _dbContext.DbConnection;
        var parameters = new
        {
            p_UserId = userId,
            p_Username = dto.Username,
            p_PasswordHash = dto.PasswordHash,
            p_RoleId = dto.RoleId,
            p_FirstName = dto.FirstName,
            p_LastName = dto.LastName,
            p_City = dto.City,
            p_Age = dto.Age,
            p_Email = dto.Email,
            p_ProfileImage = dto.ProfileImage,
            p_PhoneNumber = dto.PhoneNumber
        };

        try
        {
            await connection.ExecuteAsync("UserManagement.UpdateUserProfile",
                parameters,
                commandType: CommandType.StoredProcedure);
        
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
    }
}

