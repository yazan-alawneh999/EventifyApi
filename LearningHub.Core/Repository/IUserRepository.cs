using LearningHub.Core.Dto;



namespace LearningHub.Core.Repository;

public interface IUserRepository
{
    Task<bool> UpdateUserAsync(decimal userId , UpdateUserProfileDto dto);
}
