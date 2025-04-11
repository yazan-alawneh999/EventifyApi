using LearningHub.Core.Dto;

using LearningHubAPI.Requests;

namespace LearningHub.Core.Services;

public interface IDashboardService
{
     Task<List<RoleDto>> GetRoles();
     Task<bool> UpdateUserAsync(decimal userId, UpdateUserProfileDto dto);

}