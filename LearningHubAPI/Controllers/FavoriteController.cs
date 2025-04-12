using System.Security.Claims;
using LearningHub.Core.Response;
using LearningHub.Core.Services;
using LearningHubAPI.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearningHubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteService favoriteService;

        public FavoriteController(IFavoriteService favoriteService)
        {
            this.favoriteService = favoriteService;
        }



        [Authorize]
        [IdentityRequiresClaims(ClaimTypes.Role, new[] { "1" })]
        [HttpGet]
        [Route("getAllFavorites")]
        public List<Favorite> getAllFavorites()
        {
            return favoriteService.getAllFavorites();
        }


        [HttpGet]
        [Route("getFavoriteByID/{ID}")]
        public Favorite getFavoriteByID(int ID)
        {
            return favoriteService.getFavoriteByID(ID);
        }


        [HttpPost]
        [Route("CreateFavorite")]
        public void CreateFavorite(Favorite favorite)
        {
            favoriteService.CreateFavorite(favorite);
        }


        [HttpPut]
        [Route("UpdateFavorite")]
        public void UpdateFavorite(Favorite favorite)
        {
            favoriteService.UpdateFavorite(favorite);
        }


        [HttpDelete]
        [Route("deleteFavorite/{ID}")]
        public void deleteFavorite(int ID)
        {
            favoriteService.deleteFavorite(ID);
        }
    }
}
