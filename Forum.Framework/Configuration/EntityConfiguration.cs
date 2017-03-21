using Forum.Domain.Models;
using System;

namespace Forum.Framework.Configuration
{
    internal class EntityConfiguration<TKey, TEntity> : EntityBaseConfiguration<TKey, TEntity>
        where TKey : IComparable
        where TEntity : Entity<TKey>
    {
        public EntityConfiguration()
        {
            Property(x => x.CreatedAt).IsRequired();
            HasRequired(x => x.CreatedBy).WithMany().WillCascadeOnDelete(false);
        }
    }
}
