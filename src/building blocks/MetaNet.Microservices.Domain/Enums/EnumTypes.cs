using System.ComponentModel;

namespace MetaNet.Microservices.Domain.Enums
{
    public enum UserType
    {
        [Description("Administrador")]
        Administrator = 1,
        [Description("Usuário")]
        User = 2,
    }

    public enum SaleStatus
    {
        [Description("Aberta")]
        Open = 1,
        [Description("Fechada")]
        Closed = 2,
        [Description("Cancelada")]
        Canceled = 3
    }

    public enum PaymentFormat
    {
        [Description("")]
        Undefined = 0,
        [Description("Dinheiro")]
        Cash = 1,
        [Description("Pix")]
        Pix = 2
    }

}
