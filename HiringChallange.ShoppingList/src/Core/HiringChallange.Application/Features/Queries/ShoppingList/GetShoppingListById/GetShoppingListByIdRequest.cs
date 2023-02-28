using MediatR;
using HiringChallange.Application.DTOs.ShoppingList;

namespace HiringChallange.Application.Features.Queries.ShoppingList.GetShoppingListById
{
    public class GetShoppingListByIdRequest : IRequest<GetAllShoppingListDto>
    {
        public string Id { get; set; }
    }
}
