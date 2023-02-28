using AutoMapper;
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Identity;
using Moq;
using HiringChallange.Application.DTOs.User;
using HiringChallange.Application.Features.Commands.User.UserCreate;
using HiringChallange.Application.Mappings;
using HiringChallange.Application.Validations.User;
using HiringChallange.Domain.Common.Response;
using HiringChallange.Domain.Entities.Identity;

namespace ShoppingListProject.Test.User
{
    public class UserCreateCommandHandlerTest
    {

        private readonly IMapper _mapper;
        private readonly Mock<UserManager<AppUser>> _mockUserManager;

        public UserCreateCommandHandlerTest()
        {
            var mapperConfig = new MapperConfiguration(c => { c.AddProfile<MappingProfile>(); });
            _mapper = mapperConfig.CreateMapper();
            _mockUserManager =
                new Mock<UserManager<AppUser>>(Mock.Of<IUserStore<AppUser>>(), null, null, null, null, null, null, null, null);
        }


        [Fact]
        public void Handle_Validations_ReturnsFails()
        {
            //Arrange

            UserCreateDto userCreateDto = new UserCreateDto
            {
                Name = "a",
                Email = "a",
                Username = "a",
                Password = "",
                Surname = "a",
            };

            //Act
            UserCreateDtoValidation validator = new UserCreateDtoValidation();
            var result = validator.TestValidate(userCreateDto);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
            result.ShouldHaveValidationErrorFor(x => x.Username);
            result.ShouldHaveValidationErrorFor(x => x.Password);
            result.ShouldHaveValidationErrorFor(x => x.Email);
            result.ShouldHaveValidationErrorFor(x => x.Surname);
        }

        [Fact]
        public async Task Handle_NameAlreadyExist_ReturnBaseResponseFalse()
        {

            //Arrange

            AppUser appUser = new AppUser
            {
                Name = "Lorem",
                Email = "lorem@ipsum.com",
                UserName = "Lorem"
            };

            var handler = new UserCreateCommandHandler(
                _mockUserManager.Object,
                _mapper
                );

            UserCreateCommandRequest request = new UserCreateCommandRequest
            {
                UserCreateDto = new UserCreateDto
                {
                    Name = "Lorem",
                    Email = "lorem@ipsum.com",
                    Username = "Lorem"                   
                }
            };

            _mockUserManager.Setup(x => x.FindByNameAsync(appUser.UserName)).ReturnsAsync(appUser);

            //Act

            BaseResponse response = await handler.Handle(request, default);

            //Assert

            Assert.Equal("Kullanıcı zaten kayıtlı", response.Message);
            Assert.False(response.IsSuccess);
        }

        [Fact]
        public async Task Handle_CreateAsyncExecuted_ReturnBaseResponseSucces()
        {
            //Arrange

            AppUser appUser = new AppUser
            {
                Name = "Lorem",
                Email = "lorem@ipsum.com",
                UserName = "Lorem"
            };

            var handler = new UserCreateCommandHandler(
                _mockUserManager.Object,
                _mapper
                );

            UserCreateCommandRequest request = new UserCreateCommandRequest
            {
                UserCreateDto = new UserCreateDto
                {
                    Name = "Lorem",
                    Email = "lorem@ipsum.com",
                    Username = "LoremIpsum",
                    Password = "LoremPassword"
                }
            };

            _mockUserManager.Setup(x => x.FindByNameAsync(appUser.UserName)).ReturnsAsync(appUser);

            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
                                       .ReturnsAsync(IdentityResult.Success);

            //Act

            BaseResponse response = await handler.Handle(request, default);

            //Assert

            _mockUserManager.Verify
                (x => x.AddToRoleAsync(It.Is<AppUser>(u => u.UserName != appUser.UserName), It.IsAny<string>()));


            Assert.Equal("Kullanıcı başarıyla oluşturuldu", response.Message);
            Assert.True(response.IsSuccess);
        }

    }

}
