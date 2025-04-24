using System.Security.Claims;
using LearningHub.Core.Dto;
using LearningHub.Core.Response;
using LearningHub.Core.Services;
using LearningHubAPI.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearningHubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet]
        [Route("getallPinLocationEachEvent")]
        [IdentityRequiresClaims(ClaimTypes.Role, new[] { "1", "2", "3" })] // 1 admin, 2 org, 3 user

        public async Task<List<PinLocationEachEvent>> getallPinLocationEachEvent()
        {
            return await _locationService.getAllPinLocationEachEvent();
        }

        [HttpGet]
        [Route("getAllLocations")]
        [IdentityRequiresClaims(ClaimTypes.Role, new[] { "1", "2", "3" })] // 1 admin, 2 org, 3 user

        public Task<List<Location>> getAllLocations() 
        {
            return _locationService.getAllLocations();
        }

        [HttpGet]
        [Route("getLocationByID/{ID}")]
        [IdentityRequiresClaims(ClaimTypes.Role, new[] { "1", "2", "3" })] // 1 admin, 2 org, 3 user

        public Task<Location> getLocationByID(int ID) 
        {
            return _locationService.getLocationByID(ID);
        }
        [HttpGet]
        [Route("getLocationByEventID/{ID}")]
        [IdentityRequiresClaims(ClaimTypes.Role, new[] { "1", "2", "3" })] // 1 admin, 2 org, 3 user

        public Task<Location> getLocationByEventID(int ID)
        {
            return _locationService.getLocationByEventID(ID);
        }
        [HttpPost]
        [Route("CreateLocation")]
        [IdentityRequiresClaims(ClaimTypes.Role, new[] { "1", "2", "3" })] // 1 admin, 2 org, 3 user

        public Task<bool> createLocation(Location Location) 
        {
            return _locationService.createLocation(Location);
        } //insert
        [HttpPut]
        [Route("UpdateLocation")]
        [IdentityRequiresClaims(ClaimTypes.Role, new[] { "1", "2", "3" })] // 1 admin, 2 org, 3 user


        public Task<bool> updateLocation(Location Location) //update
        {
            return _locationService.updateLocation(Location);
        }

        [HttpDelete]
        [Route("deleteLocation/{ID}")]
        [IdentityRequiresClaims(ClaimTypes.Role, new[] { "1", "2", "3" })] // 1 admin, 2 org, 3 user

        public Task<bool> deleteLocation(int ID) //delete  
        {  return _locationService.deleteLocation(ID);}

   
    }
}
