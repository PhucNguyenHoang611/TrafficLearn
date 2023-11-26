using Microsoft.Extensions.Options;
using MongoDB.Driver;
using webapi.Models;
using webapi.Models.Settings;

namespace webapi.Services
{
    public class ExaminationServices
    {
        private readonly IMongoCollection<Examination> _examinationsCollection;
        private readonly IConfiguration _configuration;

        public ExaminationServices(IOptions<DatabaseSettings> databaseSettings, IConfiguration configuration)
        {
            _configuration = configuration;

            var connectionString = _configuration["DatabaseSettings:ConnectionString"];
            var mongoClient = new MongoClient(connectionString);

            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

            _examinationsCollection = mongoDatabase.GetCollection<Examination>(databaseSettings.Value.ExaminationsCollectionName);
        }

        public async Task<List<Examination>> GetAllExaminations() => await _examinationsCollection.Find(_ => true).ToListAsync();

        public async Task<List<Examination>> GetExaminationById(string id) => await _examinationsCollection.Find(x => x.Id == id).ToListAsync();

        public async Task CreateExamination(Examination examination) => await _examinationsCollection.InsertOneAsync(examination);

        public async Task UpdateExamination(string id, Examination examination) => await _examinationsCollection.ReplaceOneAsync(x => x.Id == id, examination);

        public async Task DeleteExamination(string id) => await _examinationsCollection.DeleteOneAsync(x => x.Id == id);
    }
}