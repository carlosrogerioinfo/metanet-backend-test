using Esterdigi.Api.Core.Commands;

namespace MetaNet.Microservices.Domain.Http.Response
{
    public class SaleResponse : ICommandResult
    {
        public Guid Id { get; set; }
        public DateTime SaleDate { get; set; }
        public double? TotalValue { get; set; }
        public string PaymentFormat { get; set; }
        public string SaleStatus { get; set; }
        public UserResponse User { get; set; }
        public IEnumerable<SaleItemResponse> SaleItems { get; set; }
        
    }
}
