using FluentValidation.TestHelper;
using Moq;
using HiringChallange.Application.Features.Commands.ShoppingList.ShoppingListCreate;
using HiringChallange.Application.Features.Commands.ShoppingList.ShoppingListDelete;
using HiringChallange.Application.Interfaces.Repositories;
using HiringChallange.Application.Validations.ShoppingList;
using HiringChallange.Domain.Common.Response;

namespace ShoppingListProject.Test.ShoppingList
{
    public class ShoppingListDeleteCommandHandlerTest
    {
        private readonly Mock<IShoppingListRepository> _mockShoppingListRepository;
        public ShoppingListDeleteCommandHandlerTest()
        {
            _mockShoppingListRepository = new();
        }

        [Fact]
        public void Handle_Validations_ReturnsFails()
        {
            //Arrange
            ShoppingListDeleteCommandRequest request = new ShoppingListDeleteCommandRequest
            {
                Id = ""
            };

            //Act
            ShoppingListDeleteCommandRequestValidation validator = new ShoppingListDeleteCommandRequestValidation();
            var result = validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Id);
        }

        [Fact]
        public async Task Handle_DeleteListExecuted_ReturnBaseResponseSucces()
        {
            //Arrange
            var handler = new ShoppingListDeleteCommandHandler(
             _mockShoppingListRepository.Object
              );

            ShoppingListDeleteCommandRequest request = new ShoppingListDeleteCommandRequest
            {
                Id = "1"
            };

            //Act

            BaseResponse response = await handler.Handle(request, default);

            //Assert

            _mockShoppingListRepository.Verify
                (x => x.DeleteAsync(request.Id), Times.Once);

            Assert.True(response.IsSuccess);
            Assert.Equal("Silme işlemi başarılı", response.Message);

        }

    }
}
