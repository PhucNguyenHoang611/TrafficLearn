using Microsoft.Extensions.Options;
using MongoDB.Driver;
using webapi.Models.Settings;
using webapi.Models;
using Azure.Security.KeyVault.Secrets;

namespace webapi.Services
{
    public class TrafficFineServices
    {
        private readonly IMongoCollection<TrafficFine> _trafficFinesCollection;
        private readonly IConfiguration _configuration;
        private readonly SecretClient _secretClient;

        public TrafficFineServices(IOptions<DatabaseSettings> databaseSettings, IConfiguration configuration, SecretClient secretClient)
        {
            _configuration = configuration;
            _secretClient = secretClient;

            /*var connectionString = _configuration["DatabaseSettings:ConnectionString"];*/
            var connectionString = _secretClient.GetSecret("DatabaseSettings-ConnectionString").Value.Value.ToString();
            var mongoClient = new MongoClient(connectionString);

            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

            _trafficFinesCollection = mongoDatabase.GetCollection<TrafficFine>(databaseSettings.Value.TrafficFinesCollectionName);
        }

        public async Task<List<TrafficFine>> GetAllTrafficFines() => await _trafficFinesCollection.Find(_ => true).ToListAsync();

        public async Task<List<TrafficFine>> GetTrafficFineById(string id) => await _trafficFinesCollection.Find(x => x.Id == id).ToListAsync();

        public async Task CreateTrafficFine(TrafficFine trafficFine) => await _trafficFinesCollection.InsertOneAsync(trafficFine);

        public async Task UpdateTrafficFine(string id, TrafficFine trafficFine) => await _trafficFinesCollection.ReplaceOneAsync(x => x.Id == id, trafficFine);

        public async Task DeleteTrafficFine(string id) => await _trafficFinesCollection.DeleteOneAsync(x => x.Id == id);
    }
}