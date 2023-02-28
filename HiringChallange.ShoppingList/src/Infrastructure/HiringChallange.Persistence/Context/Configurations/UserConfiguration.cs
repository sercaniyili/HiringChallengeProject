using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiringChallange.Domain.Entities.Identity;

namespace HiringChallange.Persistence.Context.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            var hasher = new PasswordHasher<AppUser>();
            builder.HasData(
                 new AppUser
                 {
                     Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                     Email = "admin@gmail.com",
                     NormalizedEmail = "ADMIN@GMAIL.COM",
                     Name = "System",
                     Surname = "Admin",
                     UserName = "Admınıstrator",
                     NormalizedUserName = "ADMINISTRATOR",
                     PasswordHash = hasher.HashPassword(null, "Password@123"),
                     EmailConfirmed = false
                 }
            );
        }
    }
}
