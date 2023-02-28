using HiringChallange.Domain.Common;
using HiringChallange.Domain.Entities;

namespace HiringChallange.Application.Interfaces.Repositories
{
    public interface IShoppingListRepository : IGenericRepository<ShoppingList>
    {
        Task<IEnumerable<ShoppingList>> Search(SearchQueryParameters parameters);

        Task<ShoppingList?> GetShoppingListById(string id);
    }
}
