using Esterdigi.Api.Core.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MetaNet.Microservices.Domain.Entities;

namespace MetaNet.Microservices.Infrastructure.Mappings
{
    public class SaleMap : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> entity)
        {
            //Entity
            entity.ToTable("Sales");
            entity.HasKey(x => x.Id);

            //Properties
            entity.Property(x => x.SaleDate).IsRequired().HasColumnType(Constants.DateTimePostgreSql);
            entity.Property(x => x.TotalValue).IsRequired().HasColumnType(Constants.Double);
            entity.Property(x => x.PaymentFormat).IsRequired().HasColumnType(Constants.Integer);
            entity.Property(x => x.SaleStatus).IsRequired().HasColumnType(Constants.Integer);

            //Ignore equivalent NotMapping
            entity.Ignore(x => x.Notifications);

            //Relationchip cardinality
            entity
                .HasMany(a => a.SaleItems)
                .WithOne(c => c.Sale)
                .IsRequired()
                .HasForeignKey(c => c.SaleId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}