using Microsoft.EntityFrameworkCore;
using MetaNet.Microservices.Domain.Entities;
using MetaNet.Microservices.Infrastructure.Mappings;

namespace MetaNet.Microservices.Infrastructure.Contexts
{
    public class MetaNetDataContext : DbContext
    {
        public MetaNetDataContext() { }

        public MetaNetDataContext(DbContextOptions<MetaNetDataContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //Postgre SQL
            options.UseNpgsql("Host=motty.db.elephantsql.com;Port=5432;Pooling=true;User Id=pcemciik;Password=xnog0o7cskqfpeYb2wg4sib3ZTQ-bpZB;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new SaleMap());
            modelBuilder.ApplyConfiguration(new SaleItemMap());
            modelBuilder.ApplyConfiguration(new UserMap());

        }
    }
}