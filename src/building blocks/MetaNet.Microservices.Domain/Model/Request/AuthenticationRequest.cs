using Esterdigi.Api.Core.Commands;
using System.ComponentModel.DataAnnotations;

namespace MetaNet.Microservices.Domain.Http.Request
{
    public class AuthenticationRequest :  ICommand
    {
        [Required(ErrorMessage = "O e-mail é requerido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "A senha é requerida")]
        public string Password { get; set; }
    }
}