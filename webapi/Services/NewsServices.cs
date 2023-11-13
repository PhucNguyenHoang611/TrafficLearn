using Microsoft.Extensions.Options;
using MongoDB.Driver;
using webapi.Models.Settings;
using webapi.Models;

namespace webapi.Services
{
    public class NewsServices
    {
        private readonly IMongoCollection<News> _newsCollection;
        private readonly IConfiguration _configuration;

        public NewsServices(IOptions<DatabaseSettings> databaseSettings, IConfiguration configuration)
        {
            _configuration = configuration;

            var connectionString = _configuration["DatabaseSettings:ConnectionString"];
            var mongoClient = new MongoClient(connectionString);

            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

            _newsCollection = mongoDatabase.GetCollection<News>(databaseSettings.Value.NewsCollectionName);
        }

        public async Task<List<News>> GetAllNews() => await _newsCollection.Find(_ => true).ToListAsync();

        public async Task<List<News>> GetNewsById(string id) => await _newsCollection.Find(x => x.Id == id).ToListAsync();

        public async Task CreateNews(News news) => await _newsCollection.InsertOneAsync(news);

        public async Task UpdateNews(string id, News news) => await _newsCollection.ReplaceOneAsync(x => x.Id == id, news);

        public async Task DeleteNews(string id) => await _newsCollection.DeleteOneAsync(x => x.Id == id);
    }
}