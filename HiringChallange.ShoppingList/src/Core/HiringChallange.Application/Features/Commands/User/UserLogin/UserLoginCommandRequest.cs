using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiringChallange.Application.DTOs.User;
using HiringChallange.Domain.Common.Response;

namespace HiringChallange.Application.Features.Commands.User.UserLogin
{
    public class UserLoginCommandRequest: IRequest<ServiceResponse<string>>
    {
        public UserLoginDto UserLoginDto { get; set; }
    }
}
