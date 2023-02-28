using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HiringChallange.Application.Interfaces.Contract;
using HiringChallange.Application.Interfaces.Repositories;
using HiringChallange.Domain.Entities.Identity;
using HiringChallange.Persistence.Context;
using HiringChallange.Persistence.Repositories;

namespace HiringChallange.Persistence
{
    public static class ServisRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                //options.UseLazyLoadingProxies();
            });


            services.AddTransient<IMongoConnect, MongoDbConnect>();


            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();


            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IAppUserRepository, AppUserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IShoppingListRepository, ShoppingListRepository>();

            return services;
        }

    }
}
