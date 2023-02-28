using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiringChallange.Application.Interfaces.Repositories;
using HiringChallange.Domain.Entities;
using HiringChallange.Persistence.Context;

namespace HiringChallange.Persistence.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext appDbContext) : base(appDbContext) { }
    }
}
