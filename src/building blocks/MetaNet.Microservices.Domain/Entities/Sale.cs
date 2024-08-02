using FluentValidator;
using MetaNet.Microservices.Domain.Entities.Base;
using MetaNet.Microservices.Domain.Enums;

namespace MetaNet.Microservices.Domain.Entities
{
    public class Sale : Entity
    {
        protected Sale() { }

        public Sale(Guid id, DateTime saleDate, double totalValue, PaymentFormat paymentFormat, SaleStatus saleStatus, Guid userId)
        {
            if (id != Guid.Empty) Id = id;

            SaleDate = saleDate;
            TotalValue = totalValue;
            PaymentFormat = paymentFormat;
            SaleStatus = saleStatus;
            UserId = userId;

            new ValidationContract<Sale>(this)
                ;
        }

        public DateTime SaleDate { get; private set; }
        public double? TotalValue { get; private set; }
        public PaymentFormat PaymentFormat { get; private set; }
        public SaleStatus SaleStatus { get; private set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public IEnumerable<SaleItem> SaleItems { get; } = new List<SaleItem>();

        public void CancelSale() => SaleStatus = SaleStatus.Canceled;
        public void CloseSale(double total)
        {
            SaleStatus = SaleStatus.Closed;
            TotalValue = total;
        }

        public void SetPaymentSale(PaymentFormat saleStatus) => PaymentFormat = saleStatus;
    }
}