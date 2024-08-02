using Esterdigi.Api.Core.Commands;

namespace MetaNet.Microservices.Domain.Http.Response
{
    public class StatisticResponse : ICommandResult
    {
        public int ClosedSalesCount { get; set; }
        public int CanceledSalesCount { get; set; }
        public int OpenSalesCount { get; set; }
        public int UsersCount { get; set; }
        public int ProductsCount { get; set; }
    }
}