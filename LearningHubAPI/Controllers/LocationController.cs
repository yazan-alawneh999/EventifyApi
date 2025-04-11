using LearningHub.Core.Dto;
using LearningHub.Core.Response;
using LearningHub.Core.Services;
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
        public async Task<List<PinLocationEachEvent>> getallPinLocationEachEvent()
        {
            return await _locationService.getAllPinLocationEachEvent();
        }

        [HttpGet]
        [Route("getAllLocations")]
        public Task<List<Location>> getAllLocations() 
        {
            return _locationService.getAllLocations();
        }

        [HttpGet]
        [Route("getLocationByID/{ID}")]
        public Task<Location> getLocationByID(int ID) 
        {
            return _locationService.getLocationByID(ID);
        }

        [HttpPost]
        [Route("CreateLocation")]
        public Task<bool> createLocation(Location Location) 
        {
            return _locationService.createLocation(Location);
        } //insert
        [HttpPut]
        [Route("UpdateLocation")]

        public Task<bool> updateLocation(Location Location) //update
        {
            return _locationService.updateLocation(Location);
        }

        [HttpDelete]
        [Route("deleteLocation/{ID}")]
        public Task<bool> deleteLocation(int ID) //delete  
        {  return _locationService.deleteLocation(ID);}

   
    }
}
