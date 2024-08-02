using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MetaNet.Microservices.Domain.Entities;
using Esterdigi.Api.Core.Constants;

namespace MetaNet.Microservices.Infrastructure.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            //Entity
            entity.ToTable("Users");
            entity.HasKey(x => x.Id);

            //Properties
            entity.Property(x => x.Name).IsRequired().HasMaxLength(150).HasColumnType(Constants.Varchar);
            entity.Property(x => x.Email).IsRequired().HasMaxLength(100).HasColumnType(Constants.Varchar);
            entity.Property(x => x.UserType).HasColumnType(Constants.Integer);

            //Ignore equivalent NotMapping
            entity.Ignore(x => x.Notifications);

            //Relationchip cardinality
            entity
                .HasMany(a => a.SaleUsers)
                .WithOne(c => c.User)
                .IsRequired()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}