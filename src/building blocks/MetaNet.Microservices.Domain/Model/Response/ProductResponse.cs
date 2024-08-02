using Esterdigi.Api.Core.Commands;

namespace MetaNet.Microservices.Domain.Http.Response
{
    public class ProductResponse : ICommandResult
    {
        public Guid Id { get; set; }
        public string BarCode { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
}
