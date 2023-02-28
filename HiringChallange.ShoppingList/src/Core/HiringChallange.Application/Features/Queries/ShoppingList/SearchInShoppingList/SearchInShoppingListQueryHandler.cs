using AutoMapper;
using MediatR;
using HiringChallange.Application.DTOs.ShoppingList;
using HiringChallange.Application.Interfaces.Repositories;

namespace HiringChallange.Application.Features.Queries.ShoppingList.SearchInShoppingList
{
    public class SearchInShoppingListQueryHandler : IRequestHandler<SearchInShoppingListQueryRequest, IEnumerable<GetByParameterShoppingListDto>>
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IMapper _mapper;
        public SearchInShoppingListQueryHandler(IShoppingListRepository shoppingListRepository, IMapper mapper)
        {
            _shoppingListRepository = shoppingListRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetByParameterShoppingListDto>> Handle(SearchInShoppingListQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _shoppingListRepository.Search(request.Parameters);

            return _mapper.Map<List<GetByParameterShoppingListDto>>(result);

        }
    }
}
