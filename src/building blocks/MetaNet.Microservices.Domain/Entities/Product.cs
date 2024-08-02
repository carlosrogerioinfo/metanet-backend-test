using FluentValidator;
using MetaNet.Microservices.Domain.Entities.Base;

namespace MetaNet.Microservices.Domain.Entities
{
    public class Product: Entity
    {
        protected Product() { }

        public Product(Guid id, string barcode, string description, double price)
        {
            if (id != Guid.Empty) Id = id;

            BarCode = barcode;
            Description = description;
            Price = price;

            new ValidationContract<Product>(this)
                .IsRequired(x => x.BarCode, "Código de barras deve ser informado")
                .IsRequired(x => x.Description, "Descrição da Product deve ser informada")
                .IsGreaterOrEqualsThan(x => x.Price, 1, "Preço deve ser informado")
                ;
        }

        public string BarCode { get; private set; }
        public string Description { get; private set; }
        public double Price { get; private set; }

        public IEnumerable<SaleItem> SaleItems { get; } = new List<SaleItem>();

    }
}