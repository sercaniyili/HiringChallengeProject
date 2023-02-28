using AutoMapper;
using FluentValidation.TestHelper;
using Moq;
using HiringChallange.Application.DTOs.ShoppingList;
using HiringChallange.Application.Features.Commands.ShoppingList.ShoppinListUpdate;
using HiringChallange.Application.Interfaces.Repositories;
using HiringChallange.Application.Mappings;
using HiringChallange.Application.Validations.ShoppingList;
using HiringChallange.Domain.Common.Response;
using HiringChallange.Domain.Entities;

namespace ShoppingListProject.Test.ShoppingList
{
    public class ShoppingListUpdateCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IShoppingListRepository> _mockShoppingListRepository;
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;

        public ShoppingListUpdateCommandHandlerTest()
        {
            var mapperConfig = new MapperConfiguration(c => { c.AddProfile<MappingProfile>(); });
            _mapper = mapperConfig.CreateMapper();
            _mockShoppingListRepository = new();
            _mockCategoryRepository = new();
        }

        [Fact]
        public async Task Handle_Validations_ReturnsFails()
        {
            //Arrange
            ShoppingListUpdateCommandRequest request = new ShoppingListUpdateCommandRequest
            {
                UpdateShoppingListDto = new UpdateShoppingListDto
                {
                    Id = "1"
                }
            };

            UpdateShoppingListDto updateShoppingListDto = new UpdateShoppingListDto
            {
                Id = "",
                CategoryId = "",
                Title = "",
                Description = "\"xpArXfasdqKOeShlNQVOwEcRrELRirIjrQpkwZfXEUjOXPwxdAJoBUjNMvarTNOKXtZMErmLiUjzBgBMJzlrvQRUZtELGEINwEQktLikj\\r\\n\""
            };

            var handler = new ShoppingListUpdateCommandHandler(
         _mockShoppingListRepository.Object,
         _mapper,
         _mockCategoryRepository.Object
          );

            var shoppingListId = _mockShoppingListRepository.Setup(x => x.GetByIdAsync(request.UpdateShoppingListDto.Id));

            //Act

            var result = await handler.Handle(request, default);

            UpdateShoppingListDtoValidation validator = new UpdateShoppingListDtoValidation();
            var validate = validator.TestValidate(updateShoppingListDto);

            //Assert
            Assert.NotSame(request.UpdateShoppingListDto.Id, shoppingListId);
            validate.ShouldHaveValidationErrorFor(x => x.Id);
            validate.ShouldHaveValidationErrorFor(x => x.CategoryId);
            validate.ShouldHaveValidationErrorFor(x => x.Title);
            validate.ShouldHaveValidationErrorFor(x => x.Description);

        }

        [Fact] 
        public async Task Handle_ShoppingListDoesNotExist_ReturnsBaseResponseFail()
        {
            //Arrange

            HiringChallange.Domain.Entities.ShoppingList shoppingList =
              new HiringChallange.Domain.Entities.ShoppingList
              {
                  Title = "LoremIpsum",
                  Id = "0",
                  Description = "LoremIpsum",
                  IsComplete = true,
                  CompleteDate = DateTime.Now,
                  CategoryId = "1",
                  AppUserId = "1",
                  AppUser = new HiringChallange.Domain.Entities.Identity.AppUser { },
                  Category = new HiringChallange.Domain.Entities.Category { Id = "1", Name = "Lorem" },
                  CreateDate = DateTime.Now
              };


            var handler = new ShoppingListUpdateCommandHandler(
                    _mockShoppingListRepository.Object,
                    _mapper,
                    _mockCategoryRepository.Object
                   );


            ShoppingListUpdateCommandRequest request = new ShoppingListUpdateCommandRequest
            {
                UpdateShoppingListDto = new UpdateShoppingListDto
                {
                    Id = "1",
                    CategoryId = "1",
                    Title = "Lorem",
                    Description = "LoremeIpsum"
                }
            };

            _mockShoppingListRepository.Setup(x => x.GetByIdAsync(shoppingList.Id))
                .ReturnsAsync(shoppingList);

            //Act

            BaseResponse response = await handler.Handle(request, default);

            //Assert

            Assert.False(response.IsSuccess);
            Assert.Equal("Liste bulunamadı", response.Message);

        }

        [Fact]
        public async Task Handle_CategoryDoesNotExist_ReturnsBaseResponseFail()
        {
            //Arrange

            HiringChallange.Domain.Entities.ShoppingList shoppingList =
              new HiringChallange.Domain.Entities.ShoppingList
              {
                  Title = "LoremIpsum",
                  Id = "1",
                  Description = "LoremIpsum",
                  IsComplete = true,
                  CompleteDate = DateTime.Now,
                  CategoryId = "0",
                  AppUserId = "1",
                  AppUser = new HiringChallange.Domain.Entities.Identity.AppUser { },
                  Category = new HiringChallange.Domain.Entities.Category { Id = "1", Name = "Lorem" },
                  CreateDate = DateTime.Now
              };


            var handler = new ShoppingListUpdateCommandHandler(
                    _mockShoppingListRepository.Object,
                    _mapper,
                    _mockCategoryRepository.Object
                   );


            ShoppingListUpdateCommandRequest request = new ShoppingListUpdateCommandRequest
            {
                UpdateShoppingListDto = new UpdateShoppingListDto
                {
                    Id = "1",
                    CategoryId = "1",
                    Title = "Lorem",
                    Description = "LoremeIpsum"
                }
            };

            _mockShoppingListRepository.Setup(x => x.GetByIdAsync(shoppingList.Id))
                .ReturnsAsync(shoppingList);

            _mockCategoryRepository
              .Setup(x => x.GetByIdAsync(It.IsNotIn<string>(new string[] { shoppingList.CategoryId })))
              .Returns(Task.FromResult((Category)null));

            //Act

            BaseResponse response = await handler.Handle(request, default);

            //Assert

            Assert.False(response.IsSuccess);
            Assert.Equal("Kategori bulunamadı", response.Message);

        }


        [Fact]
        public async Task Handle_UpdateListExecuted_ReturnsBaseResponseSuccess()
        {
            //Arrange

            HiringChallange.Domain.Entities.ShoppingList shoppingList =
              new HiringChallange.Domain.Entities.ShoppingList
              {
                  Title = "LoremIpsum",
                  Id = "1",
                  Description = "LoremIpsum",
                  IsComplete = true,
                  CompleteDate = DateTime.Now,
                  CategoryId = "1",
                  AppUserId = "1",
                  AppUser = new HiringChallange.Domain.Entities.Identity.AppUser { },
                  Category = new HiringChallange.Domain.Entities.Category { Id = "1", Name = "Lorem" },
                  CreateDate = DateTime.Now
              };


            var handler = new ShoppingListUpdateCommandHandler(
                    _mockShoppingListRepository.Object,
                    _mapper,
                    _mockCategoryRepository.Object
                   );


            ShoppingListUpdateCommandRequest request = new ShoppingListUpdateCommandRequest
            {
                UpdateShoppingListDto = new UpdateShoppingListDto
                {
                    Id = "1",
                    CategoryId = "1",
                    Title = "Lorem",
                    Description = "LoremeIpsum"
                }
            };

            _mockShoppingListRepository.Setup(x => x.GetByIdAsync(shoppingList.Id))
                .ReturnsAsync(shoppingList);

            _mockCategoryRepository
              .Setup(x => x.GetByIdAsync(shoppingList.CategoryId))
              .ReturnsAsync(new Category { Id = shoppingList.CategoryId, Name = "Lorem" });

            //Act

            BaseResponse response = await handler.Handle(request, default);

            //Assert

            _mockShoppingListRepository.Verify
                 (x => x.Update(It.Is<HiringChallange.Domain.Entities.ShoppingList>(s => s.Id == shoppingList.Id)), Times.Once);

            Assert.True(response.IsSuccess);
            Assert.Equal("Liste başarıyla güncellendi", response.Message);

        }

    }
}
