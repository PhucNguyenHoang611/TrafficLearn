using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
    [BsonIgnoreExtraElements]
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } = null!;

        [BsonRequired]
        [Required(ErrorMessage = "Email is required")]
        public string UserEmail { get; set; } = null!;

        [BsonRequired]
        [Required(ErrorMessage = "Password is required")]
        public string UserPassword { get; set; } = null!;

        [BsonRequired]
        [Required(ErrorMessage = "First name is required")]
        public string UserFirstName { get; set; } = null!;

        [BsonRequired]
        [Required(ErrorMessage = "Last name is required")]
        public string UserLastName { get; set; } = null!;

        public DateTime UserBirthday { get; set; }

        public string UserGender { get; set; } = string.Empty;

        public string UserPhoneNumber { get; set; } = string.Empty;

        [BsonRequired]
        [Required(ErrorMessage = "Provider is required")]
        public string UserProvider { get; set; } = null!;

        public bool IsVerified { get; set; } = false;

        [BsonRequired]
        [Required(ErrorMessage = "Role is required")]
        public string UserRole { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        public string VerificationKey { get; set; } = string.Empty;
    }
}