using M13.Domain.Entities.Base;

namespace M13.Domain.Entities
{
    public class Rule : BaseEntity
    {
        public string Site { get; set; }
        public string Principle { get; set; }
    }
}