using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
    [BsonIgnoreExtraElements]
    public class Examination
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } = null!;

        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        [Required(ErrorMessage = "License ID is required")]
        public string LicenseId { get; set; } = null!;

        [BsonRequired]
        [Required(ErrorMessage = "Examination name is required")]
        public string ExaminationName { get; set; } = null!;
    }
}