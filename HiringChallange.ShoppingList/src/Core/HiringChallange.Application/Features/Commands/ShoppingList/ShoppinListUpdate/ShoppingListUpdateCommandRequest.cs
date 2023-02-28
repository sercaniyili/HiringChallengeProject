using MediatR;
using HiringChallange.Application.DTOs.ShoppingList;
using HiringChallange.Domain.Common.Response;

namespace HiringChallange.Application.Features.Commands.ShoppingList.ShoppinListUpdate
{
    public class ShoppingListUpdateCommandRequest : IRequest<BaseResponse>
    {
        public UpdateShoppingListDto UpdateShoppingListDto { get; set; }
    }
}
