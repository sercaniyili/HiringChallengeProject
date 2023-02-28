using AutoMapper;
using FluentValidation;
using MediatR;
using HiringChallange.Application.Interfaces.Repositories;
using HiringChallange.Application.Validations.ShoppingList;
using HiringChallange.Domain.Common.Response;

namespace HiringChallange.Application.Features.Commands.ShoppingList.ShoppinListUpdate
{
    public class ShoppingListUpdateCommandHandler : IRequestHandler<ShoppingListUpdateCommandRequest, BaseResponse>
    {
        private readonly IMapper _mapper;
        private readonly IShoppingListRepository _shoppingListRepsitory;
        private readonly ICategoryRepository _categoryRepository;
        public ShoppingListUpdateCommandHandler(IShoppingListRepository shoppingListRepsitory, IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _shoppingListRepsitory = shoppingListRepsitory;
            _categoryRepository = categoryRepository;
        }

        public async Task<BaseResponse> Handle(ShoppingListUpdateCommandRequest request, CancellationToken cancellationToken)
        {
            var shoppingList = await _shoppingListRepsitory.GetByIdAsync(request.UpdateShoppingListDto.Id);
            if(shoppingList == null)
                return new BaseResponse("Liste bulunamadı", false);

            shoppingList = _mapper.Map(request.UpdateShoppingListDto, shoppingList);

            UpdateShoppingListDtoValidation validation = new UpdateShoppingListDtoValidation();
            validation.ValidateAndThrow(request.UpdateShoppingListDto);

            var category = await _categoryRepository.GetByIdAsync(request.UpdateShoppingListDto.CategoryId);
            if (category == null)
                return new BaseResponse("Kategori bulunamadı", false);

            shoppingList.Category = category;

                _shoppingListRepsitory.Update(shoppingList);
                return new BaseResponse("Liste başarıyla güncellendi", true);              
        }
    }
}
