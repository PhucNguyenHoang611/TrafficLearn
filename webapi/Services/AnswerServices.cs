using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using webapi.Models;
using webapi.Models.Settings;

namespace webapi.Services
{
    public class AnswerServices
    {
        private readonly IMongoCollection<Answer> _answersCollection;
        private readonly IConfiguration _configuration;
        private readonly SecretClient _secretClient;

        public AnswerServices(IOptions<DatabaseSettings> databaseSettings, IConfiguration configuration, SecretClient secretClient)
        {
            _configuration = configuration;
            _secretClient = secretClient;

            /*var connectionString = _configuration["DatabaseSettings:ConnectionString"];*/
            var connectionString = _secretClient.GetSecret("DatabaseSettings-ConnectionString").Value.Value.ToString();
            var mongoClient = new MongoClient(connectionString);

            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

            _answersCollection = mongoDatabase.GetCollection<Answer>(databaseSettings.Value.AnswersCollectionName);
        }

        public async Task<List<Answer>> GetAllAnswers() => await _answersCollection.Find(_ => true).ToListAsync();

        public async Task<List<Answer>> GetAnswerById(string id) => await _answersCollection.Find(x => x.Id == id).ToListAsync();

        public async Task<bool> ValidateAnswer(string questionId, string answerId)
        {
            Answer answer = await _answersCollection.Find(x => x.QuestionId == questionId && x.Id == answerId).FirstOrDefaultAsync();

            if (answer != null)
                return answer.Result;
            else
                return false;
        }

        public async Task CreateAnswer(Answer answer) => await _answersCollection.InsertOneAsync(answer);

        public async Task UpdateAnswer(string id, Answer answer) => await _answersCollection.ReplaceOneAsync(x => x.Id == id, answer);

        public async Task DeleteAnswer(string id) => await _answersCollection.DeleteOneAsync(x => x.Id == id);
    }
}