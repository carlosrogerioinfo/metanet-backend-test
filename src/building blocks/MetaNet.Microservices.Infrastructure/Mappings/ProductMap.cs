using Esterdigi.Api.Core.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MetaNet.Microservices.Domain.Entities;

namespace MetaNet.Microservices.Infrastructure.Mappings
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entity)
        {
            //Entity
            entity.ToTable("Products");
            entity.HasKey(x => x.Id);

            //Properties
            entity.Property(x => x.BarCode).IsRequired().HasMaxLength(100).HasColumnType(Constants.Varchar);
            entity.Property(x => x.Description).IsRequired().HasMaxLength(150).HasColumnType(Constants.Varchar);
            entity.Property(x => x.Price).IsRequired().HasColumnType(Constants.Double);

            //Ignore equivalent NotMapping
            entity.Ignore(x => x.Notifications);

            //Relationchip cardinality
            entity
                .HasMany(a => a.SaleItems)
                .WithOne(c => c.Product)
                .IsRequired()
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}