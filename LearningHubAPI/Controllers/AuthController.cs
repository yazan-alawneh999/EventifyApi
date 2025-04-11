using System.Security.Claims;
using LearningHub.Core.Repository;
using LearningHub.Core.Requests;
using LearningHub.Core.Services;
using LearningHubAPI.Identity;
using LearningHubAPI.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningHubAPI.Controllers;
[ApiController]
[Route("api/event-manager/auth")]
public class AuthController:ControllerBase
{
    private readonly IAuthRepo _authRepo;
    private readonly IDashboardService _dashboardService;

    public AuthController(IAuthRepo authRepo,IDashboardService dashboardService
    )
    {
        _authRepo = authRepo;
        _dashboardService = dashboardService;

        
    }
    
    
    [HttpPost("login")]
    [AllowAnonymous]
    public  async Task<IActionResult >Login([FromBody] LoginDto loginDto)
    {
       
            // Invoke the AuthService to handle login
            var loginResponse = await _authRepo.Login(loginDto.Username, loginDto.Password);
            if (loginResponse == null)
            {
                // Return 401 Unauthorized if login fails
                return Unauthorized(new { message = $"unauthorized user name: {loginDto.Username}"}); 
            }
            // On success, return the JWT token
            return Ok(loginResponse);
           
            
        }
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
     
        if (await _authRepo.UserExistsAsync(registerDto.Username))
        {
            return Conflict(new { Message = "User already exists" }); // HTTP 409 Conflict
        }

        // Attempt to register the user
        var isRegistered = await _authRepo.RegisterAsync(registerDto);

        if (!isRegistered)
        {
            return BadRequest(new { Message = "Registration failed. Please try again." }); // HTTP 400 Bad Request
        }

        return Ok(new { Message = "Registration successful" });

    }

    
    [HttpDelete("delete-user/{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        try
        {
            if (!await _authRepo.UserExistsAsync(id))
                return NotFound($"User with ID {id} does not exist.");

            var deleted = await _authRepo.DeleteUserAsync(id);

            if (!deleted)
                return StatusCode(500, $"Failed to delete user with ID {id}.");

            return Ok($"User with ID {id} deleted successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Server error: {ex.Message}");
        }
    }

  

    
    
}