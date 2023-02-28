using MediatR;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiringChallange.Application.Interfaces.Repositories;
using HiringChallange.Domain.Common;
using HiringChallange.Domain.Entities;
using HiringChallange.Persistence.Context;

namespace HiringChallange.Persistence.Repositories
{
    public class ShoppingListRepository : GenericRepository<ShoppingList>, IShoppingListRepository
    {
        private readonly AppDbContext _appDbContext;
        public ShoppingListRepository(AppDbContext appDbContext) : base(appDbContext)
        { 
            _appDbContext = appDbContext;   
        }
        protected DbSet<ShoppingList> _dbSet => _appDbContext.Set<ShoppingList>();

        public async Task<IEnumerable<ShoppingList>> Search(SearchQueryParameters parameters)
        {
            IQueryable<ShoppingList> query =  _dbSet.AsQueryable();


            if (!string.IsNullOrWhiteSpace(parameters.Keyword))
            {
                var result = query.Where(x => x.Title.Contains(parameters.Keyword.ToLower())
                ||  x.Category.Name.Contains(parameters.Keyword.ToLower()))
                .Include(x => x.Products)
                .Include(y => y.AppUser)
                .Include(z => z.Category)
                .FirstOrDefault(); 
            }

            if (parameters.Date.HasValue || parameters.Date.HasValue)
            {
               var result = query.Where(x => x.CreateDate.Equals(parameters.Date.HasValue) 
                || x.CompleteDate.Equals(parameters.Date.Value)).Include(x => x.Products)
                .Include(y => y.AppUser)
                .Include(z => z.Category)
                .FirstOrDefault(); 
            }

            return query.ToList();
        }

        public async Task<ShoppingList?> GetShoppingListById(string id)
        {
          return GetAll()
             .Where(x => x.Id == id)
             .Include(x => x.Products)
             .Include(y => y.AppUser)
             .Include(z => z.Category)
             .FirstOrDefault();
        }
    }
}
