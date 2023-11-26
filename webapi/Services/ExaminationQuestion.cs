using Microsoft.Extensions.Options;
using MongoDB.Driver;
using webapi.Models;
using webapi.Models.Settings;

namespace webapi.Services
{
    public class ExaminationQuestionServices
    {
        private readonly IMongoCollection<ExaminationQuestion> _examinationQuestionsCollection;
        private readonly IConfiguration _configuration;

        public ExaminationQuestionServices(IOptions<DatabaseSettings> databaseSettings, IConfiguration configuration)
        {
            _configuration = configuration;

            var connectionString = _configuration["DatabaseSettings:ConnectionString"];
            var mongoClient = new MongoClient(connectionString);

            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

            _examinationQuestionsCollection = mongoDatabase.GetCollection<ExaminationQuestion>(databaseSettings.Value.ExaminationQuestionsCollectionName);
        }

        public async Task<List<ExaminationQuestion>> GetAllExaminationQuestions() => await _examinationQuestionsCollection.Find(_ => true).ToListAsync();

        public async Task<List<ExaminationQuestion>> GetExaminationQuestionById(string id) => await _examinationQuestionsCollection.Find(x => x.Id == id).ToListAsync();

        public async Task CreateExaminationQuestion(ExaminationQuestion examinationQuestion) => await _examinationQuestionsCollection.InsertOneAsync(examinationQuestion);

        public async Task UpdateExaminationQuestion(string id, ExaminationQuestion examinationQuestion) => await _examinationQuestionsCollection.ReplaceOneAsync(x => x.Id == id, examinationQuestion);

        public async Task DeleteExaminationQuestion(string id) => await _examinationQuestionsCollection.DeleteOneAsync(x => x.Id == id);
    }
}