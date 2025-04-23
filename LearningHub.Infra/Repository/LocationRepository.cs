
using Dapper;
using LearningHub.Core.Common;
using LearningHub.Core.Dto;
using LearningHub.Core.Repository;
using LearningHub.Core.Response;
using Microsoft.Extensions.Configuration;

namespace LearningHub.Infra.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private readonly IDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private object _dbConnection;

        public LocationRepository(IDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<bool> createLocation(Location location)
{
    var query = @"
        INSERT INTO LOCATIONS (LATITUDE, LONGITUDE, EVENTID, ADDRESS)
        VALUES (:Latitude, :Longitude, :EventID, :Address)";

    var parameters = new
    {
        location.Latitude,
        location.Longitude,
        location.EventID,
        location.Address
    };

    using var connection = _dbContext.DbConnection;
    var result = await connection.ExecuteAsync(query, parameters);
    
    return result > 0; // ✅ التأكد من نجاح العملية
}


        public async Task<bool> deleteLocation(int id)
        {
            var query = "DELETE FROM LOCATIONS WHERE LOCATIONID = :LocationId";

            using var connection = _dbContext.DbConnection;

            var rowsAffected = await connection.ExecuteAsync(query, new { LocationId = id });

            
            return rowsAffected > 0;
        }

      

        public async Task<List<Location>> getAllLocations()
        {
            var query = "SELECT LOCATIONID, LATITUDE, LONGITUDE, ADDRESS, EVENTID FROM LOCATIONS";

            using var connection = _dbContext.DbConnection;

            var result = await connection.QueryAsync<Location>(query);

            return result.ToList();

        }

        public async Task<List<PinLocationEachEvent>> getAllPinLocationEachEvent()
        {
            var query = @"
    SELECT 
        e.EVENTID, e.ORGANIZERID, u.USERNAME AS ORGANIZERNAME, e.EVENTNAME, e.EVENTTYPE, 
        e.EVENTTIME, e.EVENTDATE, e.EVENTSTATUS, e.DESCRIPTION, 
        e.CAPACITY, e.PRICE, e.CREATEDAT, 
        l.LOCATIONID, l.LATITUDE, l.LONGITUDE, l.ADDRESS
    FROM EVENTS e
    INNER JOIN LOCATIONS l ON e.EVENTID = l.EVENTID
    INNER JOIN USERS u ON e.ORGANIZERID = u.USERID
    ";

            using var connection = _dbContext.DbConnection;

            var result = await connection.QueryAsync< PinLocationEachEvent>(
                query
                
            );

            return result.ToList();
        }



        public async Task<Location> getLocationByID(int ID)
        {
            var query = "SELECT * FROM LOCATIONS WHERE locationid = :ID";

            using var connection = _dbContext.DbConnection;

          
            var result = await connection.QuerySingleOrDefaultAsync<Location>(query, new { ID });

            return result;
        }

        public async Task<bool> updateLocation(Location location)
        {
            var query = @"
        UPDATE LOCATIONS 
        SET LATITUDE = :Latitude, 
            LONGITUDE = :Longitude, 
            ADDRESS = :Address, 
            EVENTID = :EventId
        WHERE LOCATIONID = :LocationId";

            using var connection = _dbContext.DbConnection;

           
            var rowsAffected = await connection.ExecuteAsync(query, new
            {
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                Address = location.Address,
                EventId = location.EventID,
                LocationId = location.LocationID
            });

            return rowsAffected > 0;
        }
    }
}
