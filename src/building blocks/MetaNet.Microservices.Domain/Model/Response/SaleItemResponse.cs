using Esterdigi.Api.Core.Commands;

namespace MetaNet.Microservices.Domain.Http.Response
{
    public class SaleItemResponse : ICommandResult
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Total { get; set; }

        public ProductResponse Product { get; set; }

    }
}
