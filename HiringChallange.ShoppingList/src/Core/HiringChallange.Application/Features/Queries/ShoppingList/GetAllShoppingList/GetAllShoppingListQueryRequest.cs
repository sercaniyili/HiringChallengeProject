using MediatR;
using HiringChallange.Application.DTOs.ShoppingList;

namespace HiringChallange.Application.Features.Queries.ShoppingList.GetAllShoppingList
{
    public class GetAllShoppingListQueryRequest : IRequest<List<GetAllShoppingListDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
