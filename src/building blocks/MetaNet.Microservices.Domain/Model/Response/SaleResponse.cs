using Esterdigi.Api.Core.Commands;
using MetaNet.Microservices.Domain.Enums;

namespace MetaNet.Microservices.Domain.Http.Response
{
    public class SaleResponse : ICommandResult
    {
        public Guid Id { get; set; }
        public DateTime SaleDate { get; set; }
        public double? TotalValue { get; set; }
        public PaymentFormat PaymentFormat { get; set; }
        public SaleStatus SaleStatus { get; set; }

        public IEnumerable<SaleItemResponse> SaleItems { get; set; }
    }
}
