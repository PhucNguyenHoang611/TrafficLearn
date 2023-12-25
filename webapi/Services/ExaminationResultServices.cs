using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using webapi.Models;
using webapi.Models.Settings;

namespace webapi.Services
{
    public class ExaminationResultServices
    {
        private readonly IMongoCollection<ExaminationResult> _examinationResultsCollection;
        private readonly IConfiguration _configuration;
        private readonly SecretClient _secretClient;

        public ExaminationResultServices(IOptions<DatabaseSettings> databaseSettings, IConfiguration configuration, SecretClient secretClient)
        {
            _configuration = configuration;
            _secretClient = secretClient;

            /*var connectionString = _configuration["DatabaseSettings:ConnectionString"];*/
            var connectionString = _secretClient.GetSecret("DatabaseSettings-ConnectionString").Value.Value.ToString();
            var mongoClient = new MongoClient(connectionString);

            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

            _examinationResultsCollection = mongoDatabase.GetCollection<ExaminationResult>(databaseSettings.Value.ExaminationResultsCollectionName);
        }

        public async Task<List<ExaminationResult>> GetAllExaminationResults(string userId) => await _examinationResultsCollection.Find(x => x.UserId == userId).ToListAsync();

        public async Task<List<ExaminationResult>> GetExaminationResultById(string id) => await _examinationResultsCollection.Find(x => x.Id == id).ToListAsync();

        public async Task CreateExaminationResult(ExaminationResult examinationResult) => await _examinationResultsCollection.InsertOneAsync(examinationResult);

        public async Task UpdateExaminationResult(string id, ExaminationResult examinationResult) => await _examinationResultsCollection.ReplaceOneAsync(x => x.Id == id, examinationResult);

        public async Task DeleteExaminationResult(string id) => await _examinationResultsCollection.DeleteOneAsync(x => x.Id == id);
    }
}