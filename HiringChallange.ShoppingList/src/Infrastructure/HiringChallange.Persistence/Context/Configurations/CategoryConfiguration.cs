using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HiringChallange.Domain.Entities;

namespace HiringChallange.Persistence.Context.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category
                {
                    Id = "1",
                    Name = "Market Alışverişi",
                    CreateDate = DateTime.Now
                },
                   new Category
                   {
                       Id = "2",
                       Name = "Pazar Alışverişi",
                       CreateDate = DateTime.Now
                   },
                      new Category
                      {
                          Id = "3",
                          Name = "Teknoloji Alışverişi",
                          CreateDate = DateTime.Now
                      },
                         new Category
                         {
                             Id = "4",
                             Name = "Okul Alışverişi",
                             CreateDate = DateTime.Now
                         }
            );
        }
    }
}
