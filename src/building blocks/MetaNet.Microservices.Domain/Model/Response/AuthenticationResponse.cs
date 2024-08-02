using Esterdigi.Api.Core.Commands;

namespace MetaNet.Microservices.Domain.Http.Response
{
    public class AuthenticationResponse : ICommandResult
    {
        public string Token { get; set; }
    }
}