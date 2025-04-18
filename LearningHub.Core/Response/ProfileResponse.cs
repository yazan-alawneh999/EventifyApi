namespace LearningHub.Core.Response;

public class ProfileResponse
{
    public string FirstName { get; set; }   
    public string LastName { get; set; }  
    public string City { get; set; }  
    public int Age { get; set; }  
    public string Email { get; set; }   
    public string PhoneNumber { get; set; }   
    public int RoleID  { get; set; }
    public string RoleName { get; set; }
    public string ProfileID { get; set; }
    public DateTime CreatedAt  { get; set; } 
    public string Username  { get; set; }
    

    private string _profileImage;  
    public string ProfileImage  
    {  
        get => _profileImage; 
        set => _profileImage = value;   
    }  

}