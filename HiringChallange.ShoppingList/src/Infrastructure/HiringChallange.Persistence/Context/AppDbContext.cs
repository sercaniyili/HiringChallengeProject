using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HiringChallange.Domain.Entities;
using HiringChallange.Domain.Entities.Identity;
using HiringChallange.Persistence.Context.Configurations;

namespace HiringChallange.Persistence.Context
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<ShoppingList> ShoppingLists { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());


            builder.Entity<Category>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.Entity<ShoppingList>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.Entity<Product>().Property(x => x.Id).HasDefaultValueSql("NEWID()");

            base.OnModelCreating(builder);
        }    
    }
}
