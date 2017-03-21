using Forum.Domain.Models;
using Forum.Framework.Infrastructure;
using Forum.Service.Contracts.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Forum.Service.Services.Entity
{
    public class EntityServiceBase<TKey, TEntity> : IEntityService<TKey, TEntity>
        where TKey : IComparable
        where TEntity : Entity<TKey>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<TKey, TEntity> repository;

        public EntityServiceBase(IUnitOfWork unitOfWork, IRepository<TKey, TEntity> repository)
        {
            this.unitOfWork = unitOfWork;
            this.repository = repository;
        }

        public void Create(TEntity entity)
        {
            repository.Create(entity);
        }

        public void Update(TEntity entity)
        {
            repository.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            repository.Delete(entity);
        }

        public void Delete(Expression<Func<TEntity, bool>> where)
        {
            repository.Delete(where);
        }

        public TEntity Get(TKey key)
        {
            return repository.Get(key);
        }

        public TEntity Get(TKey key, params Expression<Func<TEntity, object>>[] includes)
        {
            return repository.Get(key, includes);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> where)
        {
            return repository.Get(where);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes)
        {
            return repository.Get(where, includes);
        }

        public IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where)
        {
            return repository.GetMany(where);
        }

        public IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes)
        {
            return repository.GetMany(where, includes);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return repository.GetAll();
        }

        public IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes)
        {
            return repository.GetAll(includes);
        }

        public void CommitChanges()
        {
            unitOfWork.Commit();
        }
    }
}