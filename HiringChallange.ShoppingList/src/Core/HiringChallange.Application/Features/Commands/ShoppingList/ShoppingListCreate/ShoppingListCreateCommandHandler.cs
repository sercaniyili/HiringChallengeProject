using AutoMapper;
using FluentValidation;
using MediatR;
using HiringChallange.Application.Interfaces.Repositories;
using HiringChallange.Application.Validations.ShoppingList;
using HiringChallange.Domain.Common.Response;

namespace HiringChallange.Application.Features.Commands.ShoppingList.ShoppingListCreate
{
    public class ShoppingListCreateCommandHandler : IRequestHandler<ShoppingListCreateCommandRequest, BaseResponse>
    {

        private readonly IMapper _mapper;
        private readonly IShoppingListRepository _shoppingListRepsitory;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAppUserRepository _appUserRepository;
        public ShoppingListCreateCommandHandler(IShoppingListRepository shoppingListRepsitory, IMapper mapper, ICategoryRepository categoryRepository, IAppUserRepository appUserRepository)
        {
            _mapper = mapper;
            _shoppingListRepsitory = shoppingListRepsitory;
            _categoryRepository = categoryRepository;
            _appUserRepository = appUserRepository;
        }

        public async Task<BaseResponse> Handle(ShoppingListCreateCommandRequest request, CancellationToken cancellationToken)
        {

            var result = _mapper.Map<HiringChallange.Domain.Entities.ShoppingList>(request.CreateShoppingListDto);
           
            CreateShoppingListDtoValidation validation = new CreateShoppingListDtoValidation();
            validation.ValidateAndThrow(request.CreateShoppingListDto);

            var category = await _categoryRepository.GetByIdAsync(request.CreateShoppingListDto.CategoryId);
            if (category == null)
                return new BaseResponse("Kategori bulunamadı", false);

            var user = await _appUserRepository.GetByIdAsync(request.CreateShoppingListDto.AppUserId);
            if (user == null)
                return new BaseResponse("Kullanıcı bulunamadı", false);

            result.Category = category;
            result.AppUser = user;

            await _shoppingListRepsitory.AddAsync(result);

            return new BaseResponse("Liste başarıyla eklendi", true);
        }

    }
}


