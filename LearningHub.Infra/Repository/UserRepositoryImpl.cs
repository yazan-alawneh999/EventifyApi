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

    public  async Task<bool> UpdateProfileAsync(decimal userId, UpdateUserProfileDto dto)
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

    public async Task<bool> UpdateUserAsync(int userId, UpdateUserDto userDto)
    {
        // Validate input
        if (userDto == null) throw new ArgumentNullException(nameof(userDto));

        

        // SQL update statement
        var sql = @"
        UPDATE Users
        SET Username = :UserName,
            PasswordHash = :PasswordHash,
            RoleID = :RoleId
        WHERE UserID = :UserId";

        // Parameters for the SQL query
        var parameters = new
        {
            UserId = userId,
            UserName = userDto.UserName,
            PasswordHash = userDto.Password,
            RoleId = userDto.RoleId
        };

        using var connection = _dbContext.DbConnection;
        int rowsAffected = await connection.ExecuteAsync(sql, parameters);

        return  rowsAffected > 0;
    }

}

