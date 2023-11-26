using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
    [BsonIgnoreExtraElements]
    public class Point
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } = null!;

        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        [Required(ErrorMessage = "Clause ID is required")]
        public string ClauseId { get; set; } = null!;

        [BsonRequired]
        [Required(ErrorMessage = "Title is required")]
        public string PointTitle { get; set; } = null!;

        [BsonRequired]
        [Required(ErrorMessage = "Content is required")]
        public string PointContent { get; set; } = null!;
    }
}