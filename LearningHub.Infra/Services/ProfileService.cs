using LearningHub.Core.Dto;
using LearningHub.Core.Repository;
using LearningHub.Core.Response;
using LearningHub.Core.Services;
using LearningHub.Infra.Repository;
using Microsoft.AspNetCore.Http;

namespace LearningHub.Infra.Services;


public class ProfileService: IProfileService
{
    private readonly IProfileRepository _profileRepository;

    public ProfileService(IProfileRepository profileRepository)
    {
        _profileRepository = profileRepository;
    }

    public async Task<CreateProfileResponse?> UpdateProfile(decimal userId, ProfileDto profileDto)
    {
       return await _profileRepository.UpdateProfile(userId, profileDto);
    }

    public async Task<CreateProfileResponse?> CreateProfile(decimal userId, ProfileDto profileDto)
    {
        try
        {
            return await _profileRepository.CreateProfile(userId, profileDto);

        }
        catch (Exception e)
        {
         
            return await UpdateProfile(userId, profileDto) ;
        }
    }

    public async Task<bool> UserExistsAsync(decimal userId)
    {
       return  await _profileRepository.UserExistsAsync(userId);
    }

    public async  Task<ProfileResponse?> GetProfileByIdAsync(decimal profileId,HttpContext httpContext)
    {
        return await _profileRepository.GetProfileByIdAsync(profileId,httpContext);
    }
}