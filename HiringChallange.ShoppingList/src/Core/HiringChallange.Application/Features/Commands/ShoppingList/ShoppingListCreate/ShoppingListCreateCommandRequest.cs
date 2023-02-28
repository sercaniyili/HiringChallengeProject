using MediatR;
using HiringChallange.Application.DTOs.ShoppingList;
using HiringChallange.Domain.Common.Response;

namespace HiringChallange.Application.Features.Commands.ShoppingList.ShoppingListCreate
{
    public class ShoppingListCreateCommandRequest : IRequest<BaseResponse>
    {
        public CreateShoppingListDto CreateShoppingListDto { get; set; }
    }
}
