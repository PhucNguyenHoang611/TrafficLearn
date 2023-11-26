using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
    [BsonIgnoreExtraElements]
    public class TrafficSign
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } = null!;

        [BsonRequired]
        [Required(ErrorMessage = "Name is required")]
        public string SignName { get; set; } = null!;

        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        [Required(ErrorMessage = "Type ID is required")]
        public string SignTypeId { get; set; } = null!;

        [BsonRequired]
        [Required(ErrorMessage = "Image is required")]
        public string SignImage { get; set; } = null!;

        public string SignExplanation { get; set; } = string.Empty;
    }
}