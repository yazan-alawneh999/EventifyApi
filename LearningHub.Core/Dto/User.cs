using System.ComponentModel.DataAnnotations.Schema;

namespace LearningHub.Core.Dto;

public class User
{
    public decimal  Userid { get; set; }
    public string Username { get; set; }
    [NotMapped]
    public decimal Roleid { get; set; }
    
    
    public int? ProfileID { get; set; }
    
    public string? ProfileImage { get; set; }
    public DateTime CreateAt { get; set; }
    
    public RoleDto Role { get; set; }
    
}