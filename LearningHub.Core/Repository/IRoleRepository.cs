using LearningHub.Core.Dto;

namespace LearningHub.Core.Repository;

public interface IRoleRepository
{
    Task<List<RoleDto>> GetRoles();
}