using Forum.Domain.Models;
using System;
using System.Data.Entity.ModelConfiguration;

namespace Forum.Framework.Configuration
{
    internal class EntityBaseConfiguration<TKey, TEntity> : EntityTypeConfiguration<TEntity>
        where TKey : IComparable
        where TEntity : EntityBase<TKey>
    {
        public EntityBaseConfiguration()
        {
            HasKey(x => x.Id);
        }
    }
}
