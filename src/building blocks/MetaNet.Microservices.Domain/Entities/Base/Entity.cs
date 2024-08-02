using FluentValidator;

namespace MetaNet.Microservices.Domain.Entities.Base
{
    public abstract class Entity : Notifiable
    {
        public Guid Id { get; set; }

        public Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}
