using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
    [BsonIgnoreExtraElements]
    public class Clause
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } = null!;

        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        [Required(ErrorMessage = "Article ID is required")]
        public string ArticleId { get; set; } = null!;

        [BsonRequired]
        [Required(ErrorMessage = "Title is required")]
        public string ClauseTitle { get; set; } = null!;
    }
}