using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using LearningHub.Core.Common;
using LearningHub.Core.Dto;

using LearningHub.Core.Repository;
using LearningHub.Core.Response;
using LearningHub.Infra.Util;
using Microsoft.AspNetCore.Http;

namespace LearningHub.Infra.Repository
{
    
    public class ProfileRepository : IProfileRepository
    {
        private readonly IDbContext _dbContext;
        private readonly UtilService _utilService;
       

        public ProfileRepository(IDbContext dbContext, UtilService utilService)
        {
            _dbContext = dbContext;
            _utilService = utilService;
    
        }

        /// <summary>
        /// Create a new profile for a user.
        /// </summary>
        public async Task<CreateProfileResponse?> CreateProfile(decimal userId, ProfileDto profileDto)
        {
            await using var connection = _dbContext.DbConnection;
            await connection.OpenAsync();
            await using var transaction = await connection.BeginTransactionAsync();

           
                // ✅ 1. Save Image and Get File Path
                string imagePath = await _utilService.SaveImageAsync(profileDto.ImageFile) ?? string.Empty;

                // ✅ 2. Insert Profile Data
                var sql = @"
                    INSERT INTO Profile (UserID, FirstName, LastName, City, Age, Email, ProfileImage, PhoneNumber, CreatedAt) 
                    VALUES (:UserID, :FirstName, :LastName, :City, :Age, :Email, :ProfileImage, :PhoneNumber, SYSDATE)
                    RETURNING ProfileID INTO :InsertedID";

                var parameters = new DynamicParameters();
                parameters.Add(":UserID", userId, DbType.Decimal);
                parameters.Add(":FirstName", profileDto.FirstName, DbType.String);
                parameters.Add(":LastName", profileDto.LastName, DbType.String);
                parameters.Add(":City", profileDto.City, DbType.String);
                parameters.Add(":Age", profileDto.Age, DbType.Int32);
                parameters.Add(":Email", profileDto.Email, DbType.String);
                parameters.Add(":ProfileImage", imagePath, DbType.String);
                parameters.Add(":PhoneNumber", profileDto.PhoneNumber, DbType.String);
                parameters.Add(":InsertedID", dbType: DbType.Decimal, direction: ParameterDirection.Output);

                await connection.ExecuteAsync(sql, parameters, transaction);

                var insertedId = parameters.Get<decimal>(":InsertedID");

                if (insertedId > 0)
                {
                    await transaction.CommitAsync();
                    return new CreateProfileResponse { profileId = insertedId };
                }

                await transaction.RollbackAsync();
                return null;
            
           
        }

      /// <summary>
/// Update an existing profile.
/// </summary>
/*
public async Task<CreateProfileResponse?> UpdateProfile(decimal userId, ProfileDto userDto)
{
    await using var connection = _dbContext.DbConnection;
    await connection.OpenAsync();
    await using var transaction = await connection.BeginTransactionAsync();

    try
    {
        // ✅ 1. Save Image and Get File Path
        string imagePath = await _utilService.SaveImageAsync(userDto.ImageFile) ?? string.Empty;

        // ✅ 2. Update Profile Details (including ProfileImage)
        const string updateProfileSql = @"
            UPDATE Profile  
            SET  
                FirstName = :FirstName,  
                LastName = :LastName,  
                City = :City,  
                Age = :Age,  
                PhoneNumber = :PhoneNumber,
                ProfileImage = :ProfileImage
            WHERE  
                UserId = :UserId
            RETURNING ProfileID INTO :UpdatedID";

        var profileParams = new DynamicParameters();
        profileParams.Add(":FirstName", userDto.FirstName, DbType.String);
        profileParams.Add(":LastName", userDto.LastName, DbType.String);
        profileParams.Add(":City", userDto.City, DbType.String);
        profileParams.Add(":Age", userDto.Age, DbType.Int32);
        profileParams.Add(":PhoneNumber", userDto.PhoneNumber, DbType.String);
        profileParams.Add(":ProfileImage", imagePath, DbType.String); // ✅ Image included in one update
        profileParams.Add(":UserId", userId, DbType.Decimal);
        profileParams.Add(":UpdatedID", dbType: DbType.Decimal, direction: ParameterDirection.Output);

        await connection.ExecuteAsync(updateProfileSql, profileParams, transaction);

        
        var updatedProfileId = profileParams.Get<decimal>(":UpdatedID");

        if (updatedProfileId > 0)
        {
            await transaction.CommitAsync();
            return new CreateProfileResponse { profileId = updatedProfileId };
        }

        await transaction.RollbackAsync();
        return null;
    }
    catch (Exception ex)
    {
        await transaction.RollbackAsync();
        throw new Exception($"Error updating profile: {ex.Message}", ex);
    }
}
*/


public async Task<CreateProfileResponse?> UpdateProfile(decimal userId, ProfileDto userDto)
{
    await using var connection = _dbContext.DbConnection;
    await connection.OpenAsync();
    await using var transaction = await connection.BeginTransactionAsync();

    try
    {
        // ✅ 1. Save Image and Get File Path
        string imagePath = await _utilService.SaveImageAsync(userDto.ImageFile) ?? string.Empty;

        // ✅ 2. Update Profile
        const string updateProfileSql = @"
            UPDATE Profile  
            SET  
                FirstName = :FirstName,  
                LastName = :LastName,  
                City = :City,  
                Age = :Age,  
                PhoneNumber = :PhoneNumber,
                ProfileImage = :ProfileImage,
                Email = :Email
            WHERE  
                UserId = :UserId
            RETURNING ProfileID INTO :UpdatedID";

        var profileParams = new DynamicParameters();
        profileParams.Add(":FirstName", userDto.FirstName, DbType.String);
        profileParams.Add(":LastName", userDto.LastName, DbType.String);
        profileParams.Add(":City", userDto.City, DbType.String);
        profileParams.Add(":Age", userDto.Age, DbType.Int32);
        profileParams.Add(":PhoneNumber", userDto.PhoneNumber, DbType.String);
        profileParams.Add(":ProfileImage", imagePath, DbType.String);
        profileParams.Add(":Email", userDto.Email, DbType.String);
        profileParams.Add(":UserId", userId, DbType.Decimal);
        profileParams.Add(":UpdatedID", dbType: DbType.Decimal, direction: ParameterDirection.Output);

        await connection.ExecuteAsync(updateProfileSql, profileParams, transaction);
        var updatedProfileId = profileParams.Get<decimal>(":UpdatedID");

        // ✅ 3. Update Username and RoleID (mandatory)
        const string updateUserSql = @"
            UPDATE Users
            SET Username = :Username,
                RoleID = :RoleID
            WHERE UserID = :UserId";

        var userParams = new DynamicParameters();
        userParams.Add(":Username", userDto.Username, DbType.String);
        userParams.Add(":RoleID", userDto.RoleID, DbType.Int32);
        userParams.Add(":UserId", userId, DbType.Decimal);

        await connection.ExecuteAsync(updateUserSql, userParams, transaction);

        // ✅ 4. Commit Transaction
        if (updatedProfileId > 0)
        {
            await transaction.CommitAsync();
            return new CreateProfileResponse { profileId = updatedProfileId };
        }

        await transaction.RollbackAsync();
        return null;
    }
    catch (Exception ex)
    {
        await transaction.RollbackAsync();
        throw new Exception($"Error updating profile and user: {ex.Message}", ex);
    }
}


        /// <summary>
        /// Check if a user exists in the database.
        /// </summary>
        public async Task<bool> UserExistsAsync(decimal userId)
        {
            const string query = @"SELECT COUNT(1) FROM Users WHERE UserId = :userId";
            var parameters = new { UserId = userId };

            await using var connection = _dbContext.DbConnection;
            var count = await connection.ExecuteScalarAsync<int>(query, parameters);
            return count > 0;
        }

        public async Task<ProfileResponse?> GetProfileByIdAsync(decimal userId, HttpContext httpContext)
        {
            if (!(await UserExistsAsync(userId)))
            {
                return null;

            }
            await using var connection = _dbContext.DbConnection;
    
            string sql = @"
                        SELECT
                            u.UserID,
                            u.Username,
                            u.LastLogin,
                            r.RoleID,
                            r.RoleName,
                            p.ProfileID,
                            p.FirstName,
                            p.LastName,
                            p.City,
                            p.Age,
                            p.Email,
                            p.ProfileImage,
                            p.PhoneNumber,
                            p.CreatedAt
                        FROM
                            Users u
                        INNER JOIN 
                            Roles r ON u.RoleID = r.RoleID
                        Left JOIN
                            Profile p ON u.UserID = p.UserID
                        where
                           
                            u.UserID = :UserID";


            var profile = await connection.QueryFirstOrDefaultAsync<ProfileResponse>(sql, new { UserID = userId });

            if (profile != null && !string.IsNullOrEmpty(profile.ProfileImage))
            {
                // ✅ Generate the Base URL dynamically from the HttpContext
                string baseUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}";

                // ✅ Append the public URL to ProfileImage
                profile.ProfileImage = $"{baseUrl}/images/{profile.ProfileImage}";
            }

            return profile;
        }

    }
}
