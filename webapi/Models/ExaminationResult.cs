using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
    [BsonIgnoreExtraElements]
    public class ExaminationResult
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } = null!;

        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        [Required(ErrorMessage = "User ID is required")]
        public string UserId { get; set; } = null!;

        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        [Required(ErrorMessage = "Examination ID is required")]
        public string ExaminationId { get; set; } = null!;

        [BsonRequired]
        [Required(ErrorMessage = "Examination date is required")]
        public DateTime ExaminationDate { get; set; }

        [BsonRequired]
        [Required(ErrorMessage = "Score is required")]
        public int Score { get; set; } = 0;

        [BsonRequired]
        [Required(ErrorMessage = "IsPassed attribute is required")]
        public bool IsPassed { get; set; }
    }
}