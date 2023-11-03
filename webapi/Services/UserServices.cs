using Microsoft.Extensions.Options;
using MongoDB.Driver;
using webapi.Models;
using AspNetCore.Totp;
using AspNetCore.Totp.Interface;

namespace webapi.Services
{
    public class UserServices
    {
        private readonly IMongoCollection<User> _usersCollection;

        public UserServices (IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

            _usersCollection = mongoDatabase.GetCollection<User>(databaseSettings.Value.UsersCollectionName);
        }

        public async Task<List<User>> GetAllUsers() => await _usersCollection.Find(_ => true).ToListAsync();

        public async Task<List<User>> GetUserById(string id) => await _usersCollection.Find(x => x.Id == id).ToListAsync();

        public async Task<List<User>> GetUserByEmail(string email) => await _usersCollection.Find(x => x.UserEmail == email).ToListAsync();

        public async Task CreateUser(User user) => await _usersCollection.InsertOneAsync(user);

        public async Task UpdateUser(string id, User user) => await _usersCollection.ReplaceOneAsync(x => x.Id == id, user);

        public async Task DeleteUser(string id) => await _usersCollection.DeleteOneAsync(x => x.Id == id);

        public async Task<int> GetTOTP(string email)
        {
            List<User> users = await _usersCollection.Find(x => x.UserEmail == email).ToListAsync();
            User user = users[0];

            user.VerificationKey = Guid.NewGuid().ToString();
            await _usersCollection.ReplaceOneAsync(x => x.Id == user.Id, user);

            var totpGenerator = new TotpGenerator();
            return totpGenerator.Generate(user.VerificationKey);
        }

        public async Task<bool> ValidateTOTP(string email, int totp)
        {
            List<User> users = await _usersCollection.Find(x => x.UserEmail == email).ToListAsync();
            User user = users[0];

            var totpGenerator = new TotpGenerator();
            var totpValidator = new TotpValidator(totpGenerator);

            if (totpValidator.Validate(user.VerificationKey, totp))
            {
                user.IsVerified = true;
                await _usersCollection.ReplaceOneAsync(x => x.Id == user.Id, user);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
