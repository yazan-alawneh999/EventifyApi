using System.Security.Claims;
using LearningHub.Core.Dto;
using LearningHub.Core.Repository;
using LearningHub.Core.Services;
using LearningHubAPI.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningHubAPI.Controllers;
[ApiController]
[Route("api/event-manager/")]
public class ProfileController : ControllerBase
{

    private readonly IProfileService _profileService;

    public ProfileController(IProfileService profileService)
    {
        _profileService = profileService;
    }

    [HttpPost("create-profile/{userId}")]
    [Authorize]
    public async Task<IActionResult> CreateProfile(decimal userId, [FromForm] ProfileDto profileDto)
    {
        if (!await _profileService.UserExistsAsync(userId))
        {
            return NotFound(userId);
        }



        return Ok(await _profileService.CreateProfile(userId, profileDto));
    }

    [HttpGet("profile-details/{userId}")]
    [Authorize]
    public async Task<IActionResult> GetProfile(int userId)
    {
        var profile = await _profileService.GetProfileByIdAsync(userId, HttpContext);
        if (profile == null) return NotFound();

        return Ok(profile);
    }

}