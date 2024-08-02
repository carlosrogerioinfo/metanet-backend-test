using Esterdigi.Api.Core.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MetaNet.Microservices.Domain.Entities;

namespace MetaNet.Microservices.Infrastructure.Mappings
{
    public class SaleItemMap : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> entity)
        {
            //Entity
            entity.ToTable("SaleItems");
            entity.HasKey(x => x.Id);

            //Properties
            entity.Property(x => x.Quantity).IsRequired().HasColumnType(Constants.Integer);
            entity.Property(x => x.Price).IsRequired().HasColumnType(Constants.Double);
            entity.Property(x => x.Total).IsRequired().HasColumnType(Constants.Double);

            //Ignore equivalent NotMapping
            entity.Ignore(x => x.Notifications);

            //Relationchip cardinality

        }
    }
}