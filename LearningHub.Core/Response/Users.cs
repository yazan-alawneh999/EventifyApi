
namespace LearningHub.Core.Response
{
    public class Users
    {

        public int UserID { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public int RoleID { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}
