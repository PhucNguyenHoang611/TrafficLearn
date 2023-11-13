using Microsoft.Extensions.Options;
using MongoDB.Driver;
using webapi.Models.Settings;
using webapi.Models;

namespace webapi.Services
{
    public class TrafficFineTypeServices
    {
        private readonly IMongoCollection<TrafficFineType> _trafficFineTypesCollection;
        private readonly IConfiguration _configuration;

        public TrafficFineTypeServices(IOptions<DatabaseSettings> databaseSettings, IConfiguration configuration)
        {
            _configuration = configuration;

            var connectionString = _configuration["DatabaseSettings:ConnectionString"];
            var mongoClient = new MongoClient(connectionString);

            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

            _trafficFineTypesCollection = mongoDatabase.GetCollection<TrafficFineType>(databaseSettings.Value.TrafficFineTypesCollectionName);
        }

        public async Task<List<TrafficFineType>> GetAllTrafficFineTypes() => await _trafficFineTypesCollection.Find(_ => true).ToListAsync();

        public async Task<List<TrafficFineType>> GetTrafficFineTypeById(string id) => await _trafficFineTypesCollection.Find(x => x.Id == id).ToListAsync();

        public async Task CreateTrafficFineType(TrafficFineType trafficFineType) => await _trafficFineTypesCollection.InsertOneAsync(trafficFineType);

        public async Task UpdateTrafficFineType(string id, TrafficFineType trafficFineType) => await _trafficFineTypesCollection.ReplaceOneAsync(x => x.Id == id, trafficFineType);

        public async Task DeleteTrafficFineType(string id) => await _trafficFineTypesCollection.DeleteOneAsync(x => x.Id == id);
    }
}