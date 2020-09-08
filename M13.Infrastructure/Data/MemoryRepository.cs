using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using M13.Domain.Interfaces;

namespace M13.Infrastructure.Data
{
    public class MemoryRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        #region Private fields

        private static ConcurrentDictionary<string, TEntity> _entities;

        #endregion

        #region Ctor

        public MemoryRepository()
        {
            _entities ??= new ConcurrentDictionary<string, TEntity>();
        }

        #endregion
        
        public void Add(TEntity entity)
        {
            var id = Guid.NewGuid().ToString();
            entity.Id = id;
            _entities.TryAdd(id, entity);
        }

        public void Delete(string id)
        {
            _entities.TryRemove(id, out _);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _entities.Values;
        }
    }
}