using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using HiringChallange.Application.DTOs.ShoppingList;
using HiringChallange.Application.Features.Commands.ShoppingList.ShoppingListCreate;
using HiringChallange.Application.Features.Commands.ShoppingList.ShoppingListDelete;
using HiringChallange.Application.Features.Commands.ShoppingList.ShoppingListUpdateIsComplete;
using HiringChallange.Application.Features.Commands.ShoppingList.ShoppinListUpdate;
using HiringChallange.Application.Features.Queries.ShoppingList.GetAllShoppingList;
using HiringChallange.Application.Features.Queries.ShoppingList.GetShoppingListById;
using HiringChallange.Application.Features.Queries.ShoppingList.SearchInShoppingList;
using HiringChallange.Application.Interfaces.Cache;
using HiringChallange.Application.Interfaces.Contract;
using HiringChallange.Application.Interfaces.Repositories;

namespace HiringChallange.WebApi.Controllers
{
    [Route("api/lists")]
    [ApiController]
    public class ShoppingListController : ControllerBase
    {
        const string key = "shoppingList";
        public const string ShoppingListCollection = "CompletedLists";

        private readonly IMediator _mediator;
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IRedisDistrubutedCache _redisDistrubutedCache;
        private readonly IMongoConnect _mongoDbConnect;

        public ShoppingListController(IMediator mediator, IShoppingListRepository shoppingListRepository, IRedisDistrubutedCache redisDistrubutedCache, IMongoConnect mongoDbConnect)

            => (_mediator, _shoppingListRepository, _redisDistrubutedCache, _mongoDbConnect)
            = (mediator, shoppingListRepository, redisDistrubutedCache, mongoDbConnect);


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllShoppingLists([FromQuery] GetAllShoppingListQueryRequest request)
        {         
            var result = await _mediator.Send(request);

            await _redisDistrubutedCache.SetObjectAsync(key, result,2,60);      
           
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchInShoppingList([FromQuery] SearchInShoppingListQueryRequest request)
        {
            var result = await _mediator.Send(request);

            if (result.Any())
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetShoppingListById(string id)
        {
            var request = new GetShoppingListByIdRequest { Id = id };
            var result = await _mediator.Send(request);

            if (result != null)
                return Ok(result);
            else
                return BadRequest(result);
        }


        [HttpPost]
        public async Task<IActionResult> CreateShoppingList([FromBody] ShoppingListCreateCommandRequest request)
        {
            var result = await _mediator.Send(request);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteShoppingList([FromQuery] ShoppingListDeleteCommandRequest request)
        {
            var result = await _mediator.Send(request);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateShoppingList([FromBody] ShoppingListUpdateCommandRequest request)
        {
            var result = await _mediator.Send(request);

            return Ok(result);
        }

        [HttpPut("ıscomplete")]
        public async Task<IActionResult> UpdateShoppingListIsComplete([FromBody] ShoppingListUpdateIsCompleteCommandRequest request)
        {
            var result = await _mediator.Send(request);

            return Ok(result);
        }
        

        [HttpGet("comletedlist")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllComletedShoppingList()
        {
            var collection = _mongoDbConnect.ConnectToMongo<ShoppingListToBsonDto>(ShoppingListCollection);
            var result = await collection.Find(_=>true).ToListAsync();
            return Ok(result);
        }

    }
}
