using LearningHub.Core.Dto;
using LearningHub.Core.Response;
using Microsoft.AspNetCore.Http;

namespace LearningHub.Core.Repository;

public interface IProfileRepository
{
    Task<CreateProfileResponse?> CreateProfile(decimal userId, ProfileDto profileDto);
    Task<CreateProfileResponse?> UpdateProfile(decimal userId, ProfileDto profileDto);
     Task<bool> UserExistsAsync(decimal userId);

     Task<ProfileResponse?> GetProfileByIdAsync(decimal profileId,HttpContext httpContext);


}