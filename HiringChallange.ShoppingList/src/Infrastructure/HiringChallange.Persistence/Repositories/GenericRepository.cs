using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using HiringChallange.Application.Interfaces.Repositories;
using HiringChallange.Domain.Common;
using HiringChallange.Persistence.Context;

namespace HiringChallange.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class,IBaseEntity
    {
        protected readonly AppDbContext _appDbContext;
        
        public GenericRepository(AppDbContext appDbContext) => (_appDbContext) = (appDbContext );

        protected DbSet<T> _dbSet => _appDbContext.Set<T>();

        //Write
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var current = await _dbSet.FindAsync(id);

             _dbSet.Remove(current);
            await _appDbContext.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _appDbContext.SaveChanges();
        }


        //Read
        public IQueryable<T> GetAll()
        {
            return _dbSet.AsQueryable();
        }
        public IEnumerable<T> GetByCondition(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _dbSet.FindAsync(id);
        }
    
    }
}
