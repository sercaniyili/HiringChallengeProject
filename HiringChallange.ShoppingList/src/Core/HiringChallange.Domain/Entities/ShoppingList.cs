using HiringChallange.Domain.Common;
using HiringChallange.Domain.Entities.Identity;

namespace HiringChallange.Domain.Entities
{
    public class ShoppingList : IBaseEntity
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool IsComplete { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime? CompleteDate { get; set; }
        public virtual List<Product> Products { get; set; }

        //nav prop
        public string CategoryId { get; set; }
        public Category Category { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

    }
}
