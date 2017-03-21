using Forum.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Forum.Framework.Infrastructure
{
    public interface IRepository<TKey, TEntity>
        where TKey : IComparable
        where TEntity : EntityBase<TKey>
    {
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(Expression<Func<TEntity, bool>> where);

        TEntity Get(TKey key);
        TEntity Get(TKey key, params Expression<Func<TEntity, object>>[] includes);

        TEntity Get(Expression<Func<TEntity, bool>> where);
        TEntity Get(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes);

        IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where);
        IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes);

        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes);
    }
}