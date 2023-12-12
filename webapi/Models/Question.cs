using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
    [BsonIgnoreExtraElements]
    public class Question
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } = null!;

        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        [Required(ErrorMessage = "License title ID is required")]
        public string LicenseTitleId { get; set; } = null!;

        [BsonRequired]
        [Required(ErrorMessage = "Content is required")]
        public string QuestionContent { get; set; } = null!;

        public string QuestionMedia { get; set; } = string.Empty;

        public bool Important { get; set; } = false;

        public string Explanation { get; set; } = string.Empty;
    }
}