using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiringChallange.Application.Interfaces.Token;
using HiringChallange.Application.Validations.User;
using HiringChallange.Domain.Common.Response;
using HiringChallange.Domain.Entities.Identity;

namespace HiringChallange.Application.Features.Commands.User.UserLogin
{
    public class UserLoginCommandHandler : IRequestHandler<UserLoginCommandRequest, ServiceResponse<string>>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
       private readonly IConfiguration _configuration;
        private readonly ITokenGenerator _tokenGenerator;   
        public UserLoginCommandHandler(UserManager<AppUser> userManager, IMapper mapper, IConfiguration configuration, ITokenGenerator tokenGenerator)
        {
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<ServiceResponse<string>> Handle(UserLoginCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.UserLoginDto.Email);

            if (user == null) 
              return  new ServiceResponse<string>(string.Empty, "Kullanıcı bulunamadı", false);

            var result = await _userManager.CheckPasswordAsync(user, request.UserLoginDto.Password.Trim());
            if (!result)
                return new ServiceResponse<string>(string.Empty,"Giriş Başarısız", false);
            
            var newUser = _mapper.Map<AppUser>(request.UserLoginDto);

            UserLoginDtoValidation validation = new UserLoginDtoValidation();
            validation.ValidateAndThrow(request.UserLoginDto);

            var token = await _tokenGenerator.GenerateToken(user);
            return new ServiceResponse<string>(token ,"Giriş Başarılı", true);
            
        }
    }
}
