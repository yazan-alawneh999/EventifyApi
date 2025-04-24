using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningHub.Core.Dto;
using LearningHub.Core.Repository;
using LearningHub.Core.Response;
using LearningHub.Core.Services;

namespace LearningHub.Infra.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationService(ILocationRepository locationRepository) 
        {
            _locationRepository = locationRepository;
        }

        public Task<bool> createLocation(Location Location)
        {
            return _locationRepository.createLocation(Location);
        }

        public Task<bool> deleteLocation(int ID)
        {
            return _locationRepository.deleteLocation(ID);
        }

    

        public Task<List<Location>> getAllLocations()
        {
           return _locationRepository.getAllLocations();
        }

        public async Task<List<PinLocationEachEvent>> getAllPinLocationEachEvent()
        {
            return await _locationRepository.getAllPinLocationEachEvent();
        }

        public async Task<Location> getLocationByEventID(int id)
        {
            return await _locationRepository.getLocationByEventID(id);
        }

        public Task<Location> getLocationByID(int ID)
        {
          return _locationRepository.getLocationByID(ID);
        }

        public Task<bool> updateLocation(Location Location)
        {
           return  _locationRepository.updateLocation(Location);
        }
    }
}
