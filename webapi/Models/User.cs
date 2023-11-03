using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace webapi.Models
{
    [BsonIgnoreExtraElements]
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } = null!;

        public string UserEmail { get; set; } = null!;

        public string UserPassword { get; set; } = null!;

        public string UserFirstName { get; set; } = null!;

        public string UserLastName { get; set; } = null!;

        public DateTime UserBirthday { get; set; }

        public string UserGender { get; set; } = null!;

        public string UserPhoneNumber { get; set; } = null!;

        public string UserProvider { get; set; } = null!;

        public bool IsVerified { get; set; } = false;

        public string UserRole { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        public string VerificationKey { get; set; } = string.Empty;
    }
}
