using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HiringChallange.Application.Features.Commands.User.UserCreate;
using HiringChallange.Application.Features.Commands.User.UserLogin;
using HiringChallange.Application.Interfaces.Repositories;
using HiringChallange.Domain.Entities.Identity;
using HiringChallange.Persistence.Repositories;

namespace HiringChallange.WebApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMediator _mediator;
        public UserController(IMediator mediator, UserManager<AppUser> userManager) =>(_mediator, _userManager) = (mediator, userManager);


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserCreateCommandRequest request)
        {
             var result = await _mediator.Send(request);
            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginCommandRequest request)
        {
            var result = await _mediator.Send(request);
            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

    }
}
