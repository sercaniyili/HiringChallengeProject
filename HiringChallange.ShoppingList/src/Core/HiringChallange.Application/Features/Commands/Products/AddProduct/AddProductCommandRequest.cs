using MediatR;
using HiringChallange.Application.DTOs.Products;
using HiringChallange.Domain.Common.Response;

namespace HiringChallange.Application.Features.Commands.Products.AddProduct
{
    public class AddProductCommandRequest : IRequest<BaseResponse>
    {
        public AddProductDto AddProductDto { get; set; }
    }
}
