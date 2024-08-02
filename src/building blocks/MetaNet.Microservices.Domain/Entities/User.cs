using FluentValidator;
using MetaNet.Microservices.Domain.Entities.Base;
using MetaNet.Microservices.Domain.Enums;

namespace MetaNet.Microservices.Domain.Entities
{
    public class User: Entity
    {
        protected User() { }

        public User(Guid id, string name, string email, UserType userType, string password)
        {
            if (id != Guid.Empty) Id = id;

            Name = name;
            Email = email;
            UserType = userType;
            Password = password;

            new ValidationContract<User>(this)
                .IsRequired(x => x.Name, "O nome deve ser informado")
                .IsRequired(x => x.Password, "A senha deve ser informada")
                .IsRequired(x => x.Email, "O e-mail deve ser informado")
                .IsEmail(x => x.Email, "O e-mail informado deve ser um e-mail válido")
                ;
            
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public UserType UserType { get; private set; }
        public string Password { get; private set; }

        public IEnumerable<Sale> SaleUsers { get; } = new List<Sale>();
    }
}
