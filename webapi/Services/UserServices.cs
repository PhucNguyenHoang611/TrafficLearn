using Microsoft.Extensions.Options;
using MongoDB.Driver;
using webapi.Models;
using AspNetCore.Totp;
using AspNetCore.Totp.Interface;
using System.Security.Claims;
using System.Reflection.Emit;

namespace webapi.Services
{
    public class UserServices
    {
        private readonly IMongoCollection<User> _usersCollection;
        private readonly IConfiguration _configuration;

        public UserServices (IOptions<DatabaseSettings> databaseSettings, IConfiguration configuration)
        {
            _configuration = configuration;

            var connectionString = _configuration["DatabaseSettings:ConnectionString"];
            var mongoClient = new MongoClient(connectionString);

            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

            _usersCollection = mongoDatabase.GetCollection<User>(databaseSettings.Value.UsersCollectionName);
        }

        public async Task<string> JwtAuthentication(ClaimsIdentity identity)
        {
            if (identity != null && identity.Claims != null)
            {
                var emailClaim = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);

                if (emailClaim != null)
                {
                    string email = emailClaim.Value;
                    List<User> users = await _usersCollection.Find(x => x.UserEmail == email).ToListAsync();

                    if (users.Count == 0)
                        return "";
                    else
                        return users[0].UserRole;
                }
                else
                    return "";
            }
            else
                return "";
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
            int totp;
            
            do
            {
                totp = totpGenerator.Generate(user.VerificationKey);
            } while (totp < 100000 || totp > 999999);

            return totp;
        }

        public bool ValidateTOTP(User user, int totp)
        {
            var totpGenerator = new TotpGenerator();
            var totpValidator = new TotpValidator(totpGenerator);

            if (totpValidator.Validate(user.VerificationKey, totp))
                return true;
            else
                return false;
        }

        public async Task VerifyUser(User user)
        {
            user.IsVerified = true;
            await _usersCollection.ReplaceOneAsync(x => x.Id == user.Id, user);
        }

        public async Task UpdatePassword(User user, string newPassword)
        {
            user.UserPassword = newPassword;
            await _usersCollection.ReplaceOneAsync(x => x.Id == user.Id, user);
        }
    }
}