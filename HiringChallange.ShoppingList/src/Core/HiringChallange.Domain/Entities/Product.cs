using HiringChallange.Domain.Common;
using HiringChallange.Domain.Enums;

namespace HiringChallange.Domain.Entities
{

    public class Product : IBaseEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool? IsBuy { get; set; }
        public int? Quantity { get; set; }
        public Unit? Unit { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;

        //nav props
        public string ShoppingListId { get; set; }
    }
}
