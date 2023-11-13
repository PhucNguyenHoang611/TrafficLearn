using Microsoft.Extensions.Options;
using MongoDB.Driver;
using webapi.Models;
using webapi.Models.Settings;

namespace webapi.Services
{
    public class LicenseServices
    {
        private readonly IMongoCollection<License> _licensesCollection;
        private readonly IConfiguration _configuration;

        public LicenseServices(IOptions<DatabaseSettings> databaseSettings, IConfiguration configuration)
        {
            _configuration = configuration;

            var connectionString = _configuration["DatabaseSettings:ConnectionString"];
            var mongoClient = new MongoClient(connectionString);

            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

            _licensesCollection = mongoDatabase.GetCollection<License>(databaseSettings.Value.LicensesCollectionName);
        }

        public async Task<List<License>> GetAllLicenses() => await _licensesCollection.Find(_ => true).ToListAsync();

        public async Task<List<License>> GetLicenseById(string id) => await _licensesCollection.Find(x => x.Id == id).ToListAsync();

        public async Task CreateLicense(License license) => await _licensesCollection.InsertOneAsync(license);

        public async Task UpdateLicense(string id, License license) => await _licensesCollection.ReplaceOneAsync(x => x.Id == id, license);

        public async Task DeleteLicense(string id) => await _licensesCollection.DeleteOneAsync(x => x.Id == id);
    }
}