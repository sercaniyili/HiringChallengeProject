using MediatR;
using HiringChallange.Application.DTOs.ShoppingList;
using HiringChallange.Domain.Common.Response;

namespace HiringChallange.Application.Features.Commands.ShoppingList.ShoppingListUpdateIsComplete
{
    public class ShoppingListUpdateIsCompleteCommandRequest : IRequest<BaseResponse>
    {
        public UpdateIsCompleteDto UpdateIsCompleteDto { get; set; }
    }
}
