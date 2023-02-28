using AutoMapper;
using FluentValidation;
using MediatR;
using HiringChallange.Application.Interfaces.Repositories;
using HiringChallange.Application.Validations.Product;
using HiringChallange.Domain.Common.Response;
using HiringChallange.Domain.Entities;

namespace HiringChallange.Application.Features.Commands.Products.AddProduct
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommandRequest, BaseResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IShoppingListRepository _shoppingListRepsitory;
        public AddProductCommandHandler(IProductRepository productRepository, IMapper mapper, IShoppingListRepository shoppingListRepsitory)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _shoppingListRepsitory = shoppingListRepsitory;
        }

        public async Task<BaseResponse> Handle(AddProductCommandRequest request, CancellationToken cancellationToken)
        {
            var result = _mapper.Map<Product>(request.AddProductDto);

            AddProductDtoValidation validation = new AddProductDtoValidation();
            validation.ValidateAndThrow(request.AddProductDto);

            var shoppingList = await _shoppingListRepsitory.GetByIdAsync(request.AddProductDto.ShoppingListId);
            if (shoppingList == null)
                return new BaseResponse ("Liste bulunamadı", false);

            result.ShoppingListId = shoppingList.Id;

            await _productRepository.AddAsync(result);

            return new BaseResponse("Ürün başarıyla eklendi", true);
        }
    }                  
}
