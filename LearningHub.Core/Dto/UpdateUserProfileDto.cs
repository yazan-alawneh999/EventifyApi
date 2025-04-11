namespace LearningHub.Core.Dto;

public class UpdateUserProfileDto
{
 

    // User Fields
    public string? Username { get; set; }
    public string? PasswordHash { get; set; }
    public int? RoleId { get; set; } // Nullable now

    // Profile Fields
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? City { get; set; }
    public int? Age { get; set; }
    public string? Email { get; set; }
    public string? ProfileImage { get; set; }
    public string? PhoneNumber { get; set; }
}