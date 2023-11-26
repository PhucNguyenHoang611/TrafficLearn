using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
    [BsonIgnoreExtraElements]
    public class TrafficFine
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } = null!;

        [BsonRequired]
        [Required(ErrorMessage = "Name is required")]
        public string FineName { get; set; } = null!;

        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        [Required(ErrorMessage = "Type ID is required")]
        public string FineTypeId { get; set; } = null!;

        [BsonRequired]
        [Required(ErrorMessage = "Vehicle type is required")]
        public string VehicleType { get; set; } = null!;

        [BsonRequired]
        [Required(ErrorMessage = "Behavior is required")]
        public string FineBehavior { get; set; } = null!;

        [BsonRequired]
        [Required(ErrorMessage = "Content is required")]
        public string FineContent { get; set; } = null!;

        public string FineAdditional { get; set; } = string.Empty;

        public string FineNote { get; set; } = string.Empty;
    }
}