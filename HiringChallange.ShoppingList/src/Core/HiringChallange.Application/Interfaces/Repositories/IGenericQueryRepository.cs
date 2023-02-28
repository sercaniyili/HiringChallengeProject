using System.Linq.Expressions;
using HiringChallange.Domain.Common;

namespace HiringChallange.Application.Interfaces.Repositories
{
    public interface IGenericQueryRepository<T> where T : class, IBaseEntity
    {
        IQueryable<T> GetAll();
        Task<T> GetByIdAsync(string id);
        IEnumerable<T> GetByCondition(Expression<Func<T, bool>> expression);
    }
}
