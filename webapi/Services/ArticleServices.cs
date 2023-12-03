using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using webapi.Models;
using webapi.Models.Settings;

namespace webapi.Services
{
    public class ArticleServices
    {
        private readonly IMongoCollection<Article> _articlesCollection;
        private readonly IConfiguration _configuration;
        private readonly SecretClient _secretClient;

        public ArticleServices(IOptions<DatabaseSettings> databaseSettings, IConfiguration configuration, SecretClient secretClient)
        {
            _configuration = configuration;
            _secretClient = secretClient;

            /*var connectionString = _configuration["DatabaseSettings:ConnectionString"];*/
            var connectionString = _secretClient.GetSecret("DatabaseSettings-ConnectionString").Value.Value.ToString();
            var mongoClient = new MongoClient(connectionString);

            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

            _articlesCollection = mongoDatabase.GetCollection<Article>(databaseSettings.Value.ArticlesCollectionName);
        }

        public async Task<List<Article>> GetAllArticles() => await _articlesCollection.Find(_ => true).ToListAsync();

        public async Task<List<Article>> GetArticleById(string id) => await _articlesCollection.Find(x => x.Id == id).ToListAsync();

        public async Task CreateArticle(Article article) => await _articlesCollection.InsertOneAsync(article);

        public async Task UpdateArticle(string id, Article article) => await _articlesCollection.ReplaceOneAsync(x => x.Id == id, article);

        public async Task DeleteArticle(string id) => await _articlesCollection.DeleteOneAsync(x => x.Id == id);
    }
}