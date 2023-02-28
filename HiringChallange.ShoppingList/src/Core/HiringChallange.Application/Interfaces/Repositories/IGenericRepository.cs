using HiringChallange.Domain.Common;

namespace HiringChallange.Application.Interfaces.Repositories
{
    public interface IGenericRepository<T> : IGenericCommandRepository<T>, IGenericQueryRepository<T> where T : class, IBaseEntity { }
}
