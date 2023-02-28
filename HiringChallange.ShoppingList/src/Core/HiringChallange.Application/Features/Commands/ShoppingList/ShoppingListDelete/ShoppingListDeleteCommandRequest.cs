using MediatR;
using HiringChallange.Domain.Common.Response;

namespace HiringChallange.Application.Features.Commands.ShoppingList.ShoppingListDelete
{
    public class ShoppingListDeleteCommandRequest : IRequest<BaseResponse>
    {
        public string Id { get; set; }
    }
}
