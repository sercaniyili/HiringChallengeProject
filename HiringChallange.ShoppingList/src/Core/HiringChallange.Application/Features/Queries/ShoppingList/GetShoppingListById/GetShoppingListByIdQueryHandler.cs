using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using HiringChallange.Application.DTOs.ShoppingList;
using HiringChallange.Application.Interfaces.Repositories;

namespace HiringChallange.Application.Features.Queries.ShoppingList.GetShoppingListById
{
    public class GetShoppingListByIdQueryHandler : IRequestHandler<GetShoppingListByIdRequest, GetAllShoppingListDto>
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IMapper _mapper;

        public GetShoppingListByIdQueryHandler(IShoppingListRepository shoppingListRepository, IMapper mapper)
        {
            _shoppingListRepository = shoppingListRepository;
            _mapper = mapper;
        }

        public Task<GetAllShoppingListDto> Handle(GetShoppingListByIdRequest request, CancellationToken cancellationToken)
        {
            var result = _shoppingListRepository.GetAll().Where(x => x.Id == request.Id)
                .Include(x => x.Products)
                .Include(y => y.AppUser)
                .Include(z => z.Category)
                .FirstOrDefault();


            return Task.FromResult(_mapper.Map<GetAllShoppingListDto>(result));
        }
    }
}
