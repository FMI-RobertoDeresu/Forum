using Forum.Domain.Security;
using System.Collections.Generic;

namespace Forum.Domain.Models
{
    public class Category : Entity<int>
    {
        public string Name { get; protected set; }

        public bool IsEdited { get; protected set; }

        public virtual ICollection<Subject> Subjects { get; protected set; }

        protected Category() { }

        public Category(string name, User createdBy)
            : base(createdBy)
        {
            Name = name;
        }

        public void Edit(string newName)
        {
            Name = newName;
            IsEdited = true;
        }
    }
}