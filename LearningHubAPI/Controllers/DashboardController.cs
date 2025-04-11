using System.Security.Claims;
using LearningHub.Core.Dto;
using LearningHub.Core.Repository;
using LearningHub.Core.Services;
using LearningHubAPI.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningHubAPI.Controllers;

[ApiController]
[Route("api/event-manager/admin-dashboard")]
public class DashboardController:ControllerBase
{
    
    private readonly IAuthRepo _authRepo;
    private readonly IDashboardService _dashboardService;
    private readonly IProfileRepository _profileRepository;

    public DashboardController(IAuthRepo authRepo,IDashboardService dashboardService,IProfileRepository profileRepository)
    {
        _authRepo = authRepo;
        _dashboardService = dashboardService;
        _profileRepository = profileRepository;

        
    }
    [HttpGet("AllRegisteredUsers")]
    [Authorize]
    [IdentityRequiresClaims(ClaimTypes.Role,new[]{"1"})]
    public async Task<IActionResult> AllRegisteredUsers()
    {
        var users = await _authRepo.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("All-Roles")]
    [Authorize]
    [IdentityRequiresClaims(ClaimTypes.Role,new[]{"1"})]
    public async Task<IActionResult> GetAllRoles()
    {
        return Ok(await _dashboardService.GetRoles());
    }

    [HttpPut("UpdateProfile/{userId}")]
    [Authorize] // logged in 
    [IdentityRequiresClaims(ClaimTypes.Role, new[] { "1" })] // 1 admin ,2 org , 3 user 
    public async Task<IActionResult> UpdateProfile([FromRoute] decimal userId ,[FromForm]  ProfileDto dto)
    {
        if (!await _authRepo.UserExistsAsync(userId))
        {
            return NotFound("User not found");
        }
       
        return Ok(await _profileRepository.UpdateProfile(userId, dto));

        
    }
    
    
    [HttpPut("UpdateUser/{userId}")]
    [Authorize] // logged in 
    [IdentityRequiresClaims(ClaimTypes.Role, new[] { "1" })] // 1 admin ,2 org , 3 user 
    public async Task<IActionResult> UpdateUser([FromRoute] int userId ,[FromBody]  UpdateUserDto dto)
    {
        if (!await _authRepo.UserExistsAsync(userId))
            return NotFound("User not found");

        var updated = await _dashboardService.UpdateUserAsync(userId, dto);

        if (!updated)
            return StatusCode(500, "Failed to update user");

        return Ok(new { message = "User updated successfully" });
        
    }
    
   
}