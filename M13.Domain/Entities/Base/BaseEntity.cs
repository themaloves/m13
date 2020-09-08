using M13.Domain.Interfaces;

namespace M13.Domain.Entities.Base
{
    public class BaseEntity : IEntity
    {
        public string Id { get; set; }
    }
}