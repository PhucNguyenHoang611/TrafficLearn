using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using webapi.Models;
using webapi.Models.Settings;

namespace webapi.Services
{
    public class PointServices
    {
        private readonly IMongoCollection<Point> _pointsCollection;
        private readonly IConfiguration _configuration;
        private readonly SecretClient _secretClient;

        public PointServices(IOptions<DatabaseSettings> databaseSettings, IConfiguration configuration, SecretClient secretClient)
        {
            _configuration = configuration;
            _secretClient = secretClient;

            /*var connectionString = _configuration["DatabaseSettings:ConnectionString"];*/
            var connectionString = _secretClient.GetSecret("DatabaseSettings-ConnectionString").Value.Value.ToString();
            var mongoClient = new MongoClient(connectionString);

            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

            _pointsCollection = mongoDatabase.GetCollection<Point>(databaseSettings.Value.PointsCollectionName);
        }

        public async Task<List<Point>> GetAllPoints() => await _pointsCollection.Find(_ => true).ToListAsync();

        public async Task<List<Point>> GetPointById(string id) => await _pointsCollection.Find(x => x.Id == id).ToListAsync();

        public async Task<List<Point>> GetPointsByClauseId(string id) => await _pointsCollection.Find(x => x.ClauseId == id).ToListAsync();

        public async Task CreatePoint(Point point) => await _pointsCollection.InsertOneAsync(point);

        public async Task UpdatePoint(string id, Point point) => await _pointsCollection.ReplaceOneAsync(x => x.Id == id, point);

        public async Task DeletePoint(string id) => await _pointsCollection.DeleteOneAsync(x => x.Id == id);
    }
}