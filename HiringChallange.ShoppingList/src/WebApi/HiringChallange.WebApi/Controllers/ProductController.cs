using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HiringChallange.Application.Features.Commands.Products.AddProduct;
using HiringChallange.Application.Interfaces.Repositories;

namespace HiringChallange.WebApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator, IProductRepository productRepository)
            =>(_mediator, _productRepository)=(mediator, productRepository);

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] AddProductCommandRequest request)
        {

            var result = await _mediator.Send(request);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }
    }
}
