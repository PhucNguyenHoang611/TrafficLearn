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
        [Required(ErrorMessage = "Group is required")]
        public string TestGroup { get; set; } = null!;

        [BsonRequired]
        [Required(ErrorMessage = "Type is required")]
        public string QuestionType { get; set; } = null!;

        [BsonRequired]
        [Required(ErrorMessage = "Content is required")]
        public string QuestionContent { get; set; } = null!;

        public string QuestionMedia { get; set; } = string.Empty;

        public bool Important { get; set; } = false;

        public string Explanation { get; set; } = string.Empty;
    }
}