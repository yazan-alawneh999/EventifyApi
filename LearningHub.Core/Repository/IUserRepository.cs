using LearningHub.Core.Dto;



namespace LearningHub.Core.Repository;

public interface IUserRepository
{
    Task<bool> UpdateProfileAsync(decimal userId , UpdateUserProfileDto dto);
    Task<bool> UpdateUserAsync(int userId , UpdateUserDto dto);
    
}
