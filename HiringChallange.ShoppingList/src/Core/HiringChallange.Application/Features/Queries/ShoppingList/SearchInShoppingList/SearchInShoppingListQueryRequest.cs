using MediatR;
using HiringChallange.Application.DTOs.ShoppingList;
using HiringChallange.Domain.Common;

namespace HiringChallange.Application.Features.Queries.ShoppingList.SearchInShoppingList
{
    public class SearchInShoppingListQueryRequest : IRequest<IEnumerable<GetByParameterShoppingListDto>>
    {
        public SearchQueryParameters Parameters { get; set; }
    }
}
