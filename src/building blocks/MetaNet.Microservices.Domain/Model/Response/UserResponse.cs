using MetaNet.Microservices.Domain.Enums;
using Esterdigi.Api.Core.Commands;

namespace MetaNet.Microservices.Domain.Http.Response
{
    public class UserResponse : ICommandResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int UserTypeId { get; set; }
        public string UserType { get; set; }
    }
}