using System.Collections.Generic;

namespace M13.Domain.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity : class, IEntity
    {
        void Add(TEntity entity);
        void Delete(string id);
        IEnumerable<TEntity> GetAll();
    }
}