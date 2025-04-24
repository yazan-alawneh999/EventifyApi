using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningHub.Core.Dto;
using LearningHub.Core.Response;

namespace LearningHub.Core.Services
{
    public interface ILocationService
    {
        public Task<List<PinLocationEachEvent>> getAllPinLocationEachEvent();

        public Task<List<Location>> getAllLocations();
        public Task<Location> getLocationByID(int id);
        public Task<Location> getLocationByEventID(int id);
        public Task<bool> createLocation(Location location); //insert
        public Task<bool> updateLocation(Location location); //update
        public Task<bool> deleteLocation(int id); //delete
     
    }

}
