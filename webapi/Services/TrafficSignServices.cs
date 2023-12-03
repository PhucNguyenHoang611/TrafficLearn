using Microsoft.Extensions.Options;
using MongoDB.Driver;
using webapi.Models.Settings;
using webapi.Models;
using Azure.Security.KeyVault.Secrets;

namespace webapi.Services
{
    public class TrafficSignServices
    {
        private readonly IMongoCollection<TrafficSign> _trafficSignsCollection;
        private readonly IConfiguration _configuration;
        private readonly SecretClient _secretClient;

        public TrafficSignServices(IOptions<DatabaseSettings> databaseSettings, IConfiguration configuration, SecretClient secretClient)
        {
            _configuration = configuration;
            _secretClient = secretClient;

            /*var connectionString = _configuration["DatabaseSettings:ConnectionString"];*/
            var connectionString = _secretClient.GetSecret("DatabaseSettings-ConnectionString").Value.Value.ToString();
            var mongoClient = new MongoClient(connectionString);

            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

            _trafficSignsCollection = mongoDatabase.GetCollection<TrafficSign>(databaseSettings.Value.TrafficSignsCollectionName);
        }

        public async Task<List<TrafficSign>> GetAllTrafficSigns() => await _trafficSignsCollection.Find(_ => true).ToListAsync();

        public async Task<List<TrafficSign>> GetTrafficSignById(string id) => await _trafficSignsCollection.Find(x => x.Id == id).ToListAsync();

        public async Task CreateTrafficSign(TrafficSign trafficSign) => await _trafficSignsCollection.InsertOneAsync(trafficSign);

        public async Task UpdateTrafficSign(string id, TrafficSign trafficSign) => await _trafficSignsCollection.ReplaceOneAsync(x => x.Id == id, trafficSign);

        public async Task DeleteTrafficSign(string id) => await _trafficSignsCollection.DeleteOneAsync(x => x.Id == id);
    }
}