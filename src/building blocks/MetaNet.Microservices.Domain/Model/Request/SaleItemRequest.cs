using Esterdigi.Api.Core.Commands;

namespace MetaNet.Microservices.Domain.Http.Request
{
    public class SaleItemRegisterRequest :  ICommand
    {
        public int Quantity { get; set; }
        public double Price { get; set; }
        public Guid ProductId { get; set; }
        public Guid SaleId { get; set; }
    }

    public class SaleItemUpdateRequest : SaleItemRegisterRequest, ICommand
    {
        public Guid Id { get; set; }
    }

}