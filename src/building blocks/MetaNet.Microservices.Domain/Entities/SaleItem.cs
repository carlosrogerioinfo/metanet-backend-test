using FluentValidator;
using MetaNet.Microservices.Domain.Entities.Base;

namespace MetaNet.Microservices.Domain.Entities
{
    public class SaleItem: Entity
    {
        protected SaleItem() { }

        public SaleItem(Guid id, int quantity, double price, Guid productId, Guid saleId)
        {
            if (id != Guid.Empty) Id = id;

            Quantity = quantity;
            Price = price;
            ProductId = productId;
            SaleId = saleId;
            Total = price * quantity;

            new ValidationContract<SaleItem>(this)
                .IsGreaterThan(x => x.Quantity, 0, "Quantidade deve ser maior que zero")
                ;
        }

        public int Quantity { get; private set; }
        public double Price { get; private set; }
        public double Total { get; private set; }

        public Guid ProductId { get; private set; }
        public Product Product { get; set; }

        public Guid SaleId { get; private set; }
        public Sale Sale { get; set; }

    }
}