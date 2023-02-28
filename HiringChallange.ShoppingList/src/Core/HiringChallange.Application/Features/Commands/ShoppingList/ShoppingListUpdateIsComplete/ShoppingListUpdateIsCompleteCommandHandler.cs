using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using HiringChallange.Application.Interfaces.MessageBrokers;
using HiringChallange.Application.Interfaces.Repositories;
using HiringChallange.Application.Validations.ShoppingList;
using HiringChallange.Domain.Common.Response;

namespace HiringChallange.Application.Features.Commands.ShoppingList.ShoppingListUpdateIsComplete
{
    public class ShoppingListUpdateIsCompleteCommandHandler : IRequestHandler<ShoppingListUpdateIsCompleteCommandRequest, BaseResponse>
    {
        private readonly IMapper _mapper;
        private readonly IShoppingListRepository _shoppingListRepsitory;
        private readonly IRabbitmqService _rabbitmqService;
        public ShoppingListUpdateIsCompleteCommandHandler(IShoppingListRepository shoppingListRepsitory, IMapper mapper, IRabbitmqService rabbitmqService)
        {
            _mapper = mapper;
            _shoppingListRepsitory = shoppingListRepsitory;
            _rabbitmqService = rabbitmqService;
        }

        public async Task<BaseResponse> Handle(ShoppingListUpdateIsCompleteCommandRequest request, CancellationToken cancellationToken)
        {
            var entity = await _shoppingListRepsitory.GetShoppingListById(request.UpdateIsCompleteDto.Id);

            if (entity == null)
                return new BaseResponse("Liste tamamlama başarısız", false);

            var result = _mapper.Map(request.UpdateIsCompleteDto, entity);

            UpdateIsCompleteDtoValidation validation = new UpdateIsCompleteDtoValidation();
            validation.ValidateAndThrow(request.UpdateIsCompleteDto);

                _shoppingListRepsitory.Update(result);

                _rabbitmqService.Publish(new
                { 
                    Title = entity.Title,
                    Description = entity.Description,
                    CreateDate = entity.CreateDate,
                    IsComlete = entity.IsComplete,
                    CompleteDate = entity.CompleteDate,
                    CategoryId = entity.CategoryId,
                    AppUserId = entity.AppUserId,
                    Products = entity.Products,

                }, "direct", "direct.test", "direct.queuName", "direct.test.key");

            return new BaseResponse("Liste başarıyla tamamlandı", true);
           
               
        }
    }
}
