using LearningHub.Core.Dto;
using LearningHub.Core.Repository;
using LearningHub.Core.Services;
using LearningHubAPI.Requests;

namespace LearningHub.Infra.Services;

public class DashboardService:IDashboardService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepo;

    public DashboardService(IUserRepository userRepository, IRoleRepository roleRepo)
    {
        _userRepository = userRepository;
        _roleRepo = roleRepo;
    }


    public async Task<bool> UpdateUserAsync(decimal userId, UpdateUserProfileDto dto)
    {
        return await _userRepository.UpdateUserAsync(userId, dto);
    }

    public async Task<List<RoleDto>> GetRoles()
    {
        return await  _roleRepo.GetRoles();
    }
}