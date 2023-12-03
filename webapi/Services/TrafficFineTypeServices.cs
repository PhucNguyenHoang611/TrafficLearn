using Microsoft.Extensions.Options;
using MongoDB.Driver;
using webapi.Models.Settings;
using webapi.Models;
using Azure.Security.KeyVault.Secrets;

namespace webapi.Services
{
    public class TrafficFineTypeServices
    {
        private readonly IMongoCollection<TrafficFineType> _trafficFineTypesCollection;
        private readonly IConfiguration _configuration;
        private readonly SecretClient _secretClient;

        public TrafficFineTypeServices(IOptions<DatabaseSettings> databaseSettings, IConfiguration configuration, SecretClient secretClient)
        {
            _configuration = configuration;
            _secretClient = secretClient;

            /*var connectionString = _configuration["DatabaseSettings:ConnectionString"];*/
            var connectionString = _secretClient.GetSecret("DatabaseSettings-ConnectionString").Value.Value.ToString();
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