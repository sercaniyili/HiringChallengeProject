using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiringChallange.Application.DTOs.User;
using HiringChallange.Application.Validations.User;
using HiringChallange.Domain.Common.Response;
using HiringChallange.Domain.Entities.Identity;

namespace HiringChallange.Application.Features.Commands.User.UserCreate
{
    public class UserCreateCommandHandler : IRequestHandler<UserCreateCommandRequest, BaseResponse>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        public UserCreateCommandHandler(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<BaseResponse> Handle(UserCreateCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserCreateDto.Username);

            if (user != null)
                return new BaseResponse("Kullanıcı zaten kayıtlı", false);

            var newUser = _mapper.Map<AppUser>(request.UserCreateDto);

            UserCreateDtoValidation validation = new UserCreateDtoValidation();
            validation.ValidateAndThrow(request.UserCreateDto);

            var result = await _userManager.CreateAsync(newUser, request.UserCreateDto.Password);
            if (!result.Succeeded)
                return new BaseResponse("Kullanıcı oluşturulamadı", false);

            await _userManager.AddToRoleAsync(newUser, "User");
   
            return new BaseResponse("Kullanıcı başarıyla oluşturuldu", true);
        }
    }
}
