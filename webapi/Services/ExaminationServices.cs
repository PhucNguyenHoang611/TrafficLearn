using Microsoft.Extensions.Options;
using MongoDB.Driver;
using webapi.Models;
using webapi.Models.Settings;

namespace webapi.Services
{
    public class ExaminationServices
    {
        private readonly IMongoCollection<Examination> _examinationsCollection;
        private readonly IMongoCollection<ExaminationQuestion> _examinationQuestionsCollection;
        private readonly IMongoCollection<Question> _questionsCollection;
        private readonly IConfiguration _configuration;

        public ExaminationServices(IOptions<DatabaseSettings> databaseSettings, IConfiguration configuration)
        {
            _configuration = configuration;

            var connectionString = _configuration["DatabaseSettings:ConnectionString"];
            var mongoClient = new MongoClient(connectionString);

            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

            _examinationsCollection = mongoDatabase.GetCollection<Examination>(databaseSettings.Value.ExaminationsCollectionName);
            _examinationQuestionsCollection = mongoDatabase.GetCollection<ExaminationQuestion>(databaseSettings.Value.ExaminationQuestionsCollectionName);
            _questionsCollection = mongoDatabase.GetCollection<Question>(databaseSettings.Value.QuestionsCollectionName);
        }

        public async Task<List<Examination>> GetAllExaminations() => await _examinationsCollection.Find(_ => true).ToListAsync();

        public async Task<List<Examination>> GetExaminationById(string id) => await _examinationsCollection.Find(x => x.Id == id).ToListAsync();

        public async Task<List<Question>> GetAllQuestions(string id)
        {
            List<ExaminationQuestion> examinationQuestions = await _examinationQuestionsCollection.Find(x => x.ExaminationId == id).ToListAsync();
            List<Question> questions = new List<Question>();

            if (examinationQuestions.Count > 0)
            {
                for (int i = 0; i < examinationQuestions.Count; i++)
                {
                    Question question = await _questionsCollection.Find(x => x.Id == examinationQuestions[i].QuestionId).FirstOrDefaultAsync();

                    if (question != null)
                        questions.Add(question);
                }
            }

            return questions;
        }

        public async Task CreateExamination(Examination examination) => await _examinationsCollection.InsertOneAsync(examination);

        public async Task UpdateExamination(string id, Examination examination) => await _examinationsCollection.ReplaceOneAsync(x => x.Id == id, examination);

        public async Task DeleteExamination(string id) => await _examinationsCollection.DeleteOneAsync(x => x.Id == id);
    }
}