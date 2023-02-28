using AutoMapper;
using Moq;
using HiringChallange.Application.DTOs.ShoppingList;
using HiringChallange.Application.Features.Commands.ShoppingList.ShoppingListUpdateIsComplete;
using HiringChallange.Application.Interfaces.MessageBrokers;
using HiringChallange.Application.Interfaces.Repositories;
using HiringChallange.Application.Mappings;
using HiringChallange.Domain.Common.Response;

namespace ShoppingListProject.Test.ShoppingList
{
    public class ShoppingListUpdateIsCompleteCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IShoppingListRepository> _mockShoppingListRepository;
        private readonly Mock<IRabbitmqService> _mockRabbitmqService;

        public ShoppingListUpdateIsCompleteCommandHandlerTest()
        {
            var mapperConfig = new MapperConfiguration(c => { c.AddProfile<MappingProfile>(); });
            _mapper = mapperConfig.CreateMapper();
            _mockShoppingListRepository = new();
            _mockRabbitmqService = new();
        }

        [Fact]
        public async Task Handle_ShoppingListDoesNotExist_ReturnsBaseResponseFail()
        {
            //Arrange

            var products = new List<HiringChallange.Domain.Entities.Product>();
            products.Add(new HiringChallange.Domain.Entities.Product
            {
                Id = "1",
                Name = "Lorem",
                ShoppingListId = "0",
                CreateDate = DateTime.Now
            }); 
           

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
                  AppUser = new HiringChallange.Domain.Entities.Identity.AppUser{ },
                  Category = new HiringChallange.Domain.Entities.Category { Id ="1", Name = "Lorem"},
                  CreateDate = DateTime.Now,
                  Products = products
              };


            var handler = new ShoppingListUpdateIsCompleteCommandHandler(
                    _mockShoppingListRepository.Object,
                    _mapper,
                    _mockRabbitmqService.Object);
                                                         

            ShoppingListUpdateIsCompleteCommandRequest request = new ShoppingListUpdateIsCompleteCommandRequest
            {
                UpdateIsCompleteDto = new UpdateIsCompleteDto
                {
                    Id = "0",
                    CompleteDate = DateTime.Now,
                    IsComplete = true,
                }
            };

            _mockShoppingListRepository.Setup(x => x.GetShoppingListById(shoppingList.Id))
                .ReturnsAsync(shoppingList);

            //Act

            BaseResponse response = await handler.Handle(request, default);

            //Assert

            Assert.False(response.IsSuccess);
            Assert.Equal("Liste tamamlama başarısız",response.Message);

        }

        [Fact]
        public async Task Handle_ShoppingListExist_BaseReseResponseSuccess()
        {
            //Arrange

            var products = new List<HiringChallange.Domain.Entities.Product>();
            products.Add(new HiringChallange.Domain.Entities.Product
            {
                Id = "1",
                Name = "Lorem",
                ShoppingListId = "0",
                CreateDate = DateTime.Now
            });


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
                  CreateDate = DateTime.Now,
                  Products = products
              };


            var handler = new ShoppingListUpdateIsCompleteCommandHandler(
                    _mockShoppingListRepository.Object,
                    _mapper,
                    _mockRabbitmqService.Object);


            ShoppingListUpdateIsCompleteCommandRequest request = new ShoppingListUpdateIsCompleteCommandRequest
            {
                UpdateIsCompleteDto = new UpdateIsCompleteDto
                {
                    Id = "1",
                    CompleteDate = DateTime.Now,
                    IsComplete = true,
                }
            };

            _mockShoppingListRepository.Setup(x => x.GetShoppingListById(shoppingList.Id))
                .ReturnsAsync(shoppingList);

            //Act

            BaseResponse response = await handler.Handle(request, default);

            //Assert

            _mockShoppingListRepository.Verify
                (x=> x.Update(It.Is<HiringChallange.Domain.Entities.ShoppingList>(s=> s.Id == shoppingList.Id)), Times.Once);

            _mockRabbitmqService
                .Verify(x=> x.Publish(It.IsAny<object>(),It.IsAny<string>(),It.IsAny<string>(),It.IsAny<string>(),
                It.IsAny<string?>()),Times.Once);


            Assert.True(response.IsSuccess);
            Assert.Equal("Liste başarıyla tamamlandı", response.Message);

        }

    }
}
