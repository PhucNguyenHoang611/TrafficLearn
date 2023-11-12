using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
    [BsonIgnoreExtraElements]
    public class News
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } = null!;

        [BsonRequired]
        [Required(ErrorMessage = "Title is required")]
        public string NewsTitle { get; set; } = null!;

        [BsonRequired]
        [Required(ErrorMessage = "Clarify is required")]
        public string NewsClarify { get; set; } = null!;

        [BsonRequired]
        [Required(ErrorMessage = "Date is required")]
        public DateTime NewsDate { get; set; }

        [BsonRequired]
        [Required(ErrorMessage = "Content is required")]
        public string NewsContent { get; set; } = null!;

        public string NewsImage { get; set; } = string.Empty;

        public string NewsImageTitle { get; set; } = string.Empty;
    }
}