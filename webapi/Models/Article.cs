using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
    [BsonIgnoreExtraElements]
    public class Article
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } = null!;

        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        [Required(ErrorMessage = "Decree ID is required")]
        public string DecreeId { get; set; } = null!;

        [BsonRequired]
        [Required(ErrorMessage = "Title is required")]
        public string ArticleTitle { get; set; } = null!;
    }
}