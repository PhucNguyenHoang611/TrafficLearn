using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using webapi.Models;
using webapi.Models.Settings;

namespace webapi.Services
{
    public class TitleServices
    {
        private readonly IMongoCollection<Title> _titlesCollection;
        private readonly IConfiguration _configuration;
        private readonly SecretClient _secretClient;

        public TitleServices(IOptions<DatabaseSettings> databaseSettings, IConfiguration configuration, SecretClient secretClient)
        {
            _configuration = configuration;
            _secretClient = secretClient;

            /*var connectionString = _configuration["DatabaseSettings:ConnectionString"];*/
            var connectionString = _secretClient.GetSecret("DatabaseSettings-ConnectionString").Value.Value.ToString();
            var mongoClient = new MongoClient(connectionString);

            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

            _titlesCollection = mongoDatabase.GetCollection<Title>(databaseSettings.Value.TitlesCollectionName);
        }

        public async Task<List<Title>> GetAllTitles() => await _titlesCollection.Find(_ => true).ToListAsync();

        public async Task<List<Title>> GetTitleById(string id) => await _titlesCollection.Find(x => x.Id == id).ToListAsync();

        public async Task CreateTitle(Title title) => await _titlesCollection.InsertOneAsync(title);

        public async Task UpdateTitle(string id, Title title) => await _titlesCollection.ReplaceOneAsync(x => x.Id == id, title);

        public async Task DeleteTitle(string id) => await _titlesCollection.DeleteOneAsync(x => x.Id == id);
    }
}