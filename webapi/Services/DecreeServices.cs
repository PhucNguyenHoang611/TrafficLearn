using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using webapi.Models;
using webapi.Models.Settings;

namespace webapi.Services
{
    public class DecreeServices
    {
        private readonly IMongoCollection<Decree> _decreesCollection;
        private readonly IConfiguration _configuration;
        private readonly SecretClient _secretClient;

        public DecreeServices(IOptions<DatabaseSettings> databaseSettings, IConfiguration configuration, SecretClient secretClient)
        {
            _configuration = configuration;
            _secretClient = secretClient;

            /*var connectionString = _configuration["DatabaseSettings:ConnectionString"];*/
            var connectionString = _secretClient.GetSecret("DatabaseSettings-ConnectionString").Value.Value.ToString();
            var mongoClient = new MongoClient(connectionString);

            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

            _decreesCollection = mongoDatabase.GetCollection<Decree>(databaseSettings.Value.DecreesCollectionName);
        }

        public async Task<List<Decree>> GetAllDecrees() => await _decreesCollection.Find(_ => true).ToListAsync();

        public async Task<List<Decree>> GetDecreeById(string id) => await _decreesCollection.Find(x => x.Id == id).ToListAsync();

        public async Task CreateDecree(Decree decree) => await _decreesCollection.InsertOneAsync(decree);

        public async Task UpdateDecree(string id, Decree decree) => await _decreesCollection.ReplaceOneAsync(x => x.Id == id, decree);

        public async Task DeleteDecree(string id) => await _decreesCollection.DeleteOneAsync(x => x.Id == id);
    }
}