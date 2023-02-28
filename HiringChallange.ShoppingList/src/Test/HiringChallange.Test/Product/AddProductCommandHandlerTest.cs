using AutoMapper;
using FluentValidation.TestHelper;
using Moq;
using HiringChallange.Application.DTOs.Products;
using HiringChallange.Application.Features.Commands.Products.AddProduct;
using HiringChallange.Application.Interfaces.Repositories;
using HiringChallange.Application.Mappings;
using HiringChallange.Application.Validations.Product;
using HiringChallange.Domain.Common.Response;

namespace ShoppingListProject.Test.Product
{
    public class AddProductCommandHandlerTest
    {

        private readonly IMapper _mapper;
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly Mock<IShoppingListRepository> _mockShoppingListRepository;

        public AddProductCommandHandlerTest()
        {
            var mapperConfig = new MapperConfiguration(c => { c.AddProfile<MappingProfile>(); });
            _mapper = mapperConfig.CreateMapper();
            _mockProductRepository = new();
            _mockShoppingListRepository = new();
        }

        [Fact]
        public void Handle_Validations_ReturnsFails()
        {
            //Arrange
            AddProductDto addProductDto = new AddProductDto
            {
                Name = "",
                Quantity = 0,
                ShoppingListId = "",
            };

            //Act
            AddProductDtoValidation validator = new AddProductDtoValidation();
            var result = validator.TestValidate(addProductDto);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.ShoppingListId);
            result.ShouldHaveValidationErrorFor(x => x.Name);
            result.ShouldHaveValidationErrorFor(x => x.Quantity);
        }

        [Fact]
        public async Task Handle_ShoppingListDoesNotExist_ReturnsFail()
        {
            //Arrange
            HiringChallange.Domain.Entities.Product product = new HiringChallange.Domain.Entities.Product
            {
                Id = "1",
                Name = "Lorem",
                ShoppingListId = "0",
                CreateDate = DateTime.Now
            };

            var handler = new AddProductCommandHandler
                (
                _mockProductRepository.Object,
                _mapper,
                _mockShoppingListRepository.Object
                );

            AddProductCommandRequest request = new AddProductCommandRequest
            {
                AddProductDto = new AddProductDto
                {
                    ShoppingListId = "1",
                    Name = "Loremİpsum",
                    Quantity = 1,
                    IsBuy = true,
                    CreateDate = DateTime.Now
                }
            };

            _mockShoppingListRepository
             .Setup(x => x.GetByIdAsync(product.ShoppingListId))
             .ReturnsAsync(new HiringChallange.Domain.Entities.ShoppingList
             { Id = product.ShoppingListId, Title = "Lorem" });

            _mockShoppingListRepository
             .Setup(x => x.GetByIdAsync(It.IsNotIn<string>(new string[] { product.ShoppingListId })))
             .Returns(Task.FromResult((HiringChallange.Domain.Entities.ShoppingList)null));

            //Act
            var result = await handler.Handle(request, default);

            //Asset
            Assert.False(result.IsSuccess);
            Assert.Equal("Liste bulunamadı", result.Message);

        }

        [Fact]
        public async Task Handle_AddProductExecuted_ReturnsBaseResponseSucces()
        {
            //Arrange

            var handler = new AddProductCommandHandler
         (
         _mockProductRepository.Object,
         _mapper,
         _mockShoppingListRepository.Object
         );

            HiringChallange.Domain.Entities.Product product = new HiringChallange.Domain.Entities.Product
            {
                Id = "1",
                Name = "Lorem",
                ShoppingListId = "1",
                CreateDate = DateTime.Now
            };

            AddProductCommandRequest request = new AddProductCommandRequest
            {
                AddProductDto = new AddProductDto
                {
                    ShoppingListId = "1",
                    Name = "LoremIpsum",
                    Quantity = 1,
                    IsBuy = true,
                    CreateDate = DateTime.Now
                }
            };

            _mockShoppingListRepository
                 .Setup(x => x.GetByIdAsync(product.ShoppingListId))
                 .ReturnsAsync(new HiringChallange.Domain.Entities.ShoppingList
                 { Id = product.ShoppingListId, Title = "Lorem" });

            //Act

            BaseResponse response = await handler.Handle(request, default);

            //Assert

            _mockProductRepository.Verify
                (x => x.AddAsync(It.Is<HiringChallange.Domain.Entities.Product>
                (s=> s.ShoppingListId == product.ShoppingListId)),Times.Once);

            Assert.True(response.IsSuccess);
            Assert.Equal("Ürün başarıyla eklendi", response.Message);

        }


    }
}
