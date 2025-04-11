namespace LearningHub.Core.Dto;

public class UpdateUserDto
{
    public String UserName { get; set; }
    public String Password { get; set; }
    public int RoleId { get; set; }
}