using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using webapi.Models;
using webapi.Models.Settings;

namespace webapi.Services
{
    public class ClauseServices
    {
        private readonly IMongoCollection<Clause> _clausesCollection;
        private readonly IConfiguration _configuration;
        private readonly SecretClient _secretClient;

        public ClauseServices(IOptions<DatabaseSettings> databaseSettings, IConfiguration configuration, SecretClient secretClient)
        {
            _configuration = configuration;
            _secretClient = secretClient;

            /*var connectionString = _configuration["DatabaseSettings:ConnectionString"];*/
            var connectionString = _secretClient.GetSecret("DatabaseSettings-ConnectionString").Value.Value.ToString();
            var mongoClient = new MongoClient(connectionString);

            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

            _clausesCollection = mongoDatabase.GetCollection<Clause>(databaseSettings.Value.ClausesCollectionName);
        }

        public async Task<List<Clause>> GetAllClauses() => await _clausesCollection.Find(_ => true).ToListAsync();

        public async Task<List<Clause>> GetClauseById(string id) => await _clausesCollection.Find(x => x.Id == id).ToListAsync();

        public async Task<List<Clause>> GetClauseByArticleId(string id) => await _clausesCollection.Find(x => x.ArticleId == id).ToListAsync();

        public async Task CreateClause(Clause clause) => await _clausesCollection.InsertOneAsync(clause);

        public async Task UpdateClause(string id, Clause clause) => await _clausesCollection.ReplaceOneAsync(x => x.Id == id, clause);

        public async Task DeleteClause(string id) => await _clausesCollection.DeleteOneAsync(x => x.Id == id);
    }
}