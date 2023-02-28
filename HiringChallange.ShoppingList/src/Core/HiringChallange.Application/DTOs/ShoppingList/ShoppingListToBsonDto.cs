using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using HiringChallange.Domain.Entities;

namespace HiringChallange.Application.DTOs.ShoppingList
{

    public class ShoppingListToBsonDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool IsComplete { get; set; } = true;
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime? CompleteDate { get; set; }
        public List<Product> Products { get; set; }
        public string CategoryId { get; set; }
        public string AppUserId { get; set; }

    }
}
