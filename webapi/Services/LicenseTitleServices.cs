using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using webapi.Models;
using webapi.Models.Settings;

namespace webapi.Services
{
    public class LicenseTitleServices
    {
        private readonly IMongoCollection<LicenseTitle> _licenseTitlesCollection;
        private readonly IConfiguration _configuration;
        private readonly SecretClient _secretClient;

        public LicenseTitleServices(IOptions<DatabaseSettings> databaseSettings, IConfiguration configuration, SecretClient secretClient)
        {
            _configuration = configuration;
            _secretClient = secretClient;

            /*var connectionString = _configuration["DatabaseSettings:ConnectionString"];*/
            var connectionString = _secretClient.GetSecret("DatabaseSettings-ConnectionString").Value.Value.ToString();
            var mongoClient = new MongoClient(connectionString);

            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

            _licenseTitlesCollection = mongoDatabase.GetCollection<LicenseTitle>(databaseSettings.Value.LicenseTitlesCollectionName);
        }

        public async Task<List<LicenseTitle>> GetAllLicenseTitles() => await _licenseTitlesCollection.Find(_ => true).ToListAsync();

        public async Task<List<LicenseTitle>> GetLicenseTitleById(string id) => await _licenseTitlesCollection.Find(x => x.Id == id).ToListAsync();

        public async Task CreateLicenseTitle(LicenseTitle licenseTitle) => await _licenseTitlesCollection.InsertOneAsync(licenseTitle);

        public async Task UpdateLicenseTitle(string id, LicenseTitle licenseTitle) => await _licenseTitlesCollection.ReplaceOneAsync(x => x.Id == id, licenseTitle);

        public async Task DeleteLicenseTitle(string id) => await _licenseTitlesCollection.DeleteOneAsync(x => x.Id == id);
    }
}