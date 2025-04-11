using LearningHub.Core.Dto;

using LearningHubAPI.Requests;

namespace LearningHub.Core.Services;

public interface IDashboardService
{
     Task<List<RoleDto>> GetRoles();
     Task<bool> UpdateProfileAsync(decimal userId, UpdateUserProfileDto dto);
     Task<bool> UpdateUserAsync(int userId, UpdateUserDto dto);

}