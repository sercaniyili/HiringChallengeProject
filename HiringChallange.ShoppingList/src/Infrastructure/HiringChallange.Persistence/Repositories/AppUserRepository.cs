using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiringChallange.Application.Interfaces.Repositories;
using HiringChallange.Domain.Entities.Identity;
using HiringChallange.Persistence.Context;

namespace HiringChallange.Persistence.Repositories
{
    public class AppUserRepository : GenericRepository<AppUser>, IAppUserRepository
    {
        public AppUserRepository(AppDbContext appDbContext) : base(appDbContext) { }
    }
}
