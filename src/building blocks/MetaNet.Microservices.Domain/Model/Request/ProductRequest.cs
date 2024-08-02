using Esterdigi.Api.Core.Commands;

namespace MetaNet.Microservices.Domain.Http.Request
{
    public class ProductRegisterRequest :  ICommand
    {
        public string BarCode { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }

    public class ProductUpdateRequest : ProductRegisterRequest, ICommand
    {
        public Guid Id { get; set; }
    }

}
