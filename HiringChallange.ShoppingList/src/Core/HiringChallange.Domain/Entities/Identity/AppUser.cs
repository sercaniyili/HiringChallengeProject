using Microsoft.AspNetCore.Identity;
using HiringChallange.Domain.Common;

namespace HiringChallange.Domain.Entities.Identity
{
    public class AppUser : IdentityUser, IBaseEntity
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public ICollection<ShoppingList> ShoppingLists { get; set; }
    }
}
