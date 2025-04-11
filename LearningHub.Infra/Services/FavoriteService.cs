
using LearningHub.Core.Response;
using LearningHub.Core.Services;
using LearningHub.Infra.Repository;

namespace LearningHub.Infra.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoriteRepository _favoriteRepository;

        public FavoriteService(IFavoriteRepository favoriteRepository)
        {
            _favoriteRepository = favoriteRepository;
        }
        public void CreateFavorite(Favorite favorite)
        {
            _favoriteRepository.CreateFavorite(favorite);
        }

        public void deleteFavorite(int ID)
        {
            _favoriteRepository.deleteFavorite(ID);
        }

        public List<Favorite> getAllFavorites()
        {
           return _favoriteRepository.getAllFavorites();
        }

        public Favorite getFavoriteByID(int ID)
        {
           return _favoriteRepository.getFavoriteByID(ID);
        }

        public void UpdateFavorite(Favorite favorite)
        {
            _favoriteRepository.UpdateFavorite(favorite);
        }
    }
}
