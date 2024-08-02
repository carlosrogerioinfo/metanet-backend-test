using Esterdigi.Api.Core.Commands;
using MetaNet.Microservices.Domain.Enums;

namespace MetaNet.Microservices.Domain.Http.Request
{
    public class SaleRegisterRequest :  ICommand
    {
        public Guid UserId { get; set; }
    }

    public class SaleUpdateRequest: ICommand
    {
        public Guid Id { get; set; }
        public PaymentFormat PaymentFormat { get; set; }

    }

}