using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningHub.Core.Response;

namespace LearningHub.Core.Services
{
    public interface IFavoriteService
    {
        public List<Favorite> getAllFavorites();
        public Favorite getFavoriteByID(int ID);
        public void CreateFavorite(Favorite favorite);
        public void UpdateFavorite(Favorite favorite);
        public void deleteFavorite(int ID);
    }
}
