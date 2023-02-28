using HiringChallange.Domain.Common;

namespace HiringChallange.Domain.Entities
{
    public class Category : IBaseEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public ICollection<ShoppingList> ShoppingLists { get; set; }
    }
}
