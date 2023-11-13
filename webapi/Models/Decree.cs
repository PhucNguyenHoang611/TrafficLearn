using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
    [BsonIgnoreExtraElements]
    public class Decree
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } = null!;

        [BsonRequired]
        [Required(ErrorMessage = "Name is required")]
        public string DecreeName { get; set; } = null!;

        [BsonRequired]
        [Required(ErrorMessage = "Date is required")]
        public DateTime DecreeDate { get; set; }

        [BsonRequired]
        [Required(ErrorMessage = "Number is required")]
        public string DecreeNumber { get; set; } = null!;
    }
}