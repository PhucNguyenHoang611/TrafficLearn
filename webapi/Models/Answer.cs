using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
    [BsonIgnoreExtraElements]
    public class Answer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } = null!;

        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        [Required(ErrorMessage = "Question ID is required")]
        public string QuestionId { get; set; } = null!;

        [BsonRequired]
        [Required(ErrorMessage = "Content is required")]
        public string AnswerContent { get; set; } = null!;

        [BsonRequired]
        [Required(ErrorMessage = "Result is required")]
        public bool Result { get; set; }
    }
}