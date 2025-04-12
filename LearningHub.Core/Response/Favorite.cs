

namespace LearningHub.Core.Response
{
    public class Favorite
    {

        public int FavoriteID { get; set; }
        public int EventID { get; set; }
        public int UserID { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
