using LearningHub.Core.Dto;
using LearningHub.Core.Response;

using Microsoft.AspNetCore.Http;

namespace LearningHub.Core.Services;

public interface IProfileService
{
    Task<CreateProfileResponse?>  UpdateProfile(decimal profileId,ProfileDto profileDto);
    Task<CreateProfileResponse?> CreateProfile(decimal userId,ProfileDto profileDto);

    Task<bool> UserExistsAsync(decimal userId);

    Task<ProfileResponse?> GetProfileByIdAsync(decimal profileId,HttpContext httpContext);


}