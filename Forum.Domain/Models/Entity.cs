using Forum.Domain.Security;
using System;

namespace Forum.Domain.Models
{
    public abstract class Entity<TKey> : EntityBase<TKey>
        where TKey : IComparable
    {
        public DateTime CreatedAt { get; protected set; }

        public User CreatedBy { get; protected set; }

        protected Entity()
        {
            CreatedAt = DateTime.Now;
        }

        public Entity(User createdBy)
            : this()
        {
            CreatedBy = createdBy;
        }
    }
}