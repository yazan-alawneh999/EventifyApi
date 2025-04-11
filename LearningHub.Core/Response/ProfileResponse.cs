namespace LearningHub.Core.Response;

public class ProfileResponse
{
    public string FirstName { get; set; }   
    public string LastName { get; set; }  
    public string City { get; set; }  
    public int Age { get; set; }  
    public string Email { get; set; }   
    public string PhoneNumber { get; set; }   

    private string _profileImage;  
    public string ProfileImage  
    {  
        get => _profileImage; // Return the stored file name only  
        set => _profileImage = value; // Store only file name  
    }  

}