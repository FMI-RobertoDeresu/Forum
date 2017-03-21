using System;

namespace Forum.Domain.Models
{
    public abstract class EntityBase<TKey>
        where TKey : IComparable
    {
        public TKey Id { get; protected set; }
    }
}
