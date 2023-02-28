using AutoMapper;
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using HiringChallange.Application.DTOs.User;
using HiringChallange.Application.Features.Commands.User.UserCreate;
using HiringChallange.Application.Features.Commands.User.UserLogin;
using HiringChallange.Application.Interfaces.Token;
using HiringChallange.Application.Mappings;
using HiringChallange.Application.Validations.User;
using HiringChallange.Domain.Common.Response;
using HiringChallange.Domain.Entities.Identity;


namespace ShoppingListProject.Test.User
{
    public class UserLoginCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<UserManager<AppUser>> _mockUserManager;
        private readonly Mock<ITokenGenerator> _mockTokenGenerator;
        private readonly Mock<IConfiguration> _mockConfiguration;

        public UserLoginCommandHandlerTest()
        {
            var mapperConfig = new MapperConfiguration(c => { c.AddProfile<MappingProfile>(); });
            _mapper = mapperConfig.CreateMapper();
            _mockUserManager =
            new Mock<UserManager<AppUser>>(Mock.Of<IUserStore<AppUser>>(), null, null, null, null, null, null, null, null);
            _mockTokenGenerator = new();
            _mockConfiguration = new();
        }


        [Fact]
        public void Handle_Validations_ReturnsFails()
        {
            //Arrange

            UserLoginDto userLoginDto = new UserLoginDto
            {
                Email = "a",
                Password = ""
            };

            //Act
            UserLoginDtoValidation validator = new UserLoginDtoValidation();
            var result = validator.TestValidate(userLoginDto);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Email);
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public async Task Handle_EmailDoesNotExist_ReturnServiceResponseFail()
        {
            //Arrange

            AppUser appUser = new AppUser
            {
                Name = "Lorem",
                Email = "lorem@ipsum.com",
                UserName = "Lorem"
            };

            var handler = new UserLoginCommandHandler(
                _mockUserManager.Object,
                _mapper,
                _mockConfiguration.Object,
                _mockTokenGenerator.Object
                );

            UserLoginCommandRequest request = new UserLoginCommandRequest
            {
                UserLoginDto = new UserLoginDto
                {
                    Password = null,
                    Email = null
                }
            };

            _mockUserManager.Setup(x => x.FindByEmailAsync(It.Is<string>(x=> x == appUser.Email)))
                .ReturnsAsync((AppUser)appUser);


            //Act

            ServiceResponse<string> response = await handler.Handle(request, default);

            //Assert
            Assert.Equal(response.Value, string.Empty);
            Assert.Equal("Kullanıcı bulunamadı", response.Message);
            Assert.False(response.IsSuccess);

        }

        [Fact]
        public async Task Handle_CheckPasswordFalse_ReturnServiceResponseFail()
        {
            //Arrange
 
            AppUser appUser = new AppUser
            {
                Name = "Lorem",
                Email = "lorem@ipsum.com",
                UserName = "Lorem",
                PasswordHash = "LoremHash"
            };

            var handler = new UserLoginCommandHandler(
                _mockUserManager.Object,
                _mapper,
                _mockConfiguration.Object,
                _mockTokenGenerator.Object
                );

            UserLoginCommandRequest request = new UserLoginCommandRequest
            {
                UserLoginDto = new UserLoginDto
                {
                    Password = "Lorem",
                    Email = "lorem@ipsum.com"
                }
            };

            _mockUserManager.Setup(x => x.FindByEmailAsync(It.Is<string>(x => x == appUser.Email)))
                .ReturnsAsync((AppUser)appUser);

            _mockUserManager.Setup
                (x => x.CheckPasswordAsync(It.Is<AppUser>(x=> x.Email == appUser.Email),It.IsAny<string>()))
                .ReturnsAsync(appUser.PasswordHash == request.UserLoginDto.Password);


            //Act
            ServiceResponse<string> response = await handler.Handle(request, default);

            //Assert
            Assert.Equal(response.Value, string.Empty);
            Assert.Equal("Giriş Başarısız", response.Message);
            Assert.False(response.IsSuccess);

        }

        [Fact]
        public async Task Handle_CheckPasswordTrue_ReturnServiceWithToken()
        {
            //Arrange

            AppUser appUser = new AppUser
            {
                Name = "Lorem",
                Email = "lorem@ipsum.com",
                UserName = "Lorem",
                PasswordHash = "Lorem"
            };

            var handler = new UserLoginCommandHandler(
                _mockUserManager.Object,
                _mapper,
                _mockConfiguration.Object,
                _mockTokenGenerator.Object
                );

            UserLoginCommandRequest request = new UserLoginCommandRequest
            {
                UserLoginDto = new UserLoginDto
                {
                    Password = "Lorem",
                    Email = "lorem@ipsum.com"
                }
            };

            _mockUserManager.Setup(x => x.FindByEmailAsync(It.Is<string>(x => x == appUser.Email)))
                .ReturnsAsync((AppUser)appUser);

            _mockUserManager.Setup
                (x => x.CheckPasswordAsync(It.Is<AppUser>(x => x.Email == appUser.Email), It.IsAny<string>()))
                .ReturnsAsync(appUser.PasswordHash == request.UserLoginDto.Password);

            _mockTokenGenerator.Setup(x => x.GenerateToken(It.Is<AppUser>(x => x.Email == appUser.Email)))
                .ReturnsAsync("token");

            //Act
            ServiceResponse<string> response = await handler.Handle(request, default);

            //Assert
            Assert.Equal(response.Value,"token");
            Assert.Equal("Giriş Başarılı", response.Message);
            Assert.True(response.IsSuccess);

        }
    }
}
