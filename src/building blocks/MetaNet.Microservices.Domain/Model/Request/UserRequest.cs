using MetaNet.Microservices.Domain.Enums;
using Esterdigi.Api.Core.Commands;

namespace MetaNet.Microservices.Domain.Http.Request
{
    public class UserRegisterRequest :  ICommand
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public UserType UserType { get; set; }
    }

    public class UserUpdateRequest : UserRegisterRequest, ICommand
    {
        public Guid Id { get; set; }
    }
}