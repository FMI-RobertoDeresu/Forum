using Forum.Domain.Models;
using Forum.Framework.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Forum.Framework.Infrastructure
{
    public class RepositoryBase<TKey, TEntity> : IRepository<TKey, TEntity>
        where TKey : IComparable
        where TEntity : EntityBase<TKey>
    {
        private IDbDactory dbFactory;
        private ForumDbContext dbContext;
        private IDbSet<TEntity> dbSet;

        protected DbContext DbContext
        {
            get { return dbContext ?? (dbContext = dbFactory.Init()); }
        }

        protected RepositoryBase(IDbDactory dbFactory)
        {
            this.dbFactory = dbFactory;
            this.dbSet = DbContext.Set<TEntity>();
        }

        public virtual void Create(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            dbSet.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(TEntity entity)
        {
            dbSet.Remove(entity);
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> where)
        {
            IEnumerable<TEntity> objects = dbSet.Where(where).AsEnumerable();
            foreach (TEntity obj in objects)
                dbSet.Remove(obj);
        }

        public virtual TEntity Get(TKey id)
        {
            return dbSet.Find(id);
        }

        public TEntity Get(TKey key, params Expression<Func<TEntity, object>>[] includes)
        {
            return dbSet.IncludeMany(includes).FirstOrDefault(x => x.Id.ToString() == key.ToString());
        }

        public virtual TEntity Get(Expression<Func<TEntity, bool>> where)
        {
            return Get(where, null);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes)
        {
            return dbSet.IncludeMany(includes).FirstOrDefault(where);
        }

        public virtual IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where)
        {
            return GetMany(where, null);
        }

        public IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes)
        {
            return dbSet.IncludeMany(includes).Where(where).ToList();
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return GetAll(null);
        }

        public IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes)
        {
            return dbSet.IncludeMany(includes).ToList();
        }
    }
}
