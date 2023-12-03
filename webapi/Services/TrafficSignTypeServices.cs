using Microsoft.Extensions.Options;
using MongoDB.Driver;
using webapi.Models.Settings;
using webapi.Models;
using Azure.Security.KeyVault.Secrets;

namespace webapi.Services
{
    public class TrafficSignTypeServices
    {
        private readonly IMongoCollection<TrafficSignType> _trafficSignTypesCollection;
        private readonly IConfiguration _configuration;
        private readonly SecretClient _secretClient;

        public TrafficSignTypeServices(IOptions<DatabaseSettings> databaseSettings, IConfiguration configuration, SecretClient secretClient)
        {
            _configuration = configuration;
            _secretClient = secretClient;

            /*var connectionString = _configuration["DatabaseSettings:ConnectionString"];*/
            var connectionString = _secretClient.GetSecret("DatabaseSettings-ConnectionString").Value.Value.ToString();
            var mongoClient = new MongoClient(connectionString);

            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

            _trafficSignTypesCollection = mongoDatabase.GetCollection<TrafficSignType>(databaseSettings.Value.TrafficSignTypesCollectionName);
        }

        public async Task<List<TrafficSignType>> GetAllTrafficSignTypes() => await _trafficSignTypesCollection.Find(_ => true).ToListAsync();

        public async Task<List<TrafficSignType>> GetTrafficSignTypeById(string id) => await _trafficSignTypesCollection.Find(x => x.Id == id).ToListAsync();

        public async Task CreateTrafficSignType(TrafficSignType trafficSignType) => await _trafficSignTypesCollection.InsertOneAsync(trafficSignType);

        public async Task UpdateTrafficSignType(string id, TrafficSignType trafficSignType) => await _trafficSignTypesCollection.ReplaceOneAsync(x => x.Id == id, trafficSignType);

        public async Task DeleteTrafficSignType(string id) => await _trafficSignTypesCollection.DeleteOneAsync(x => x.Id == id);
    }
}