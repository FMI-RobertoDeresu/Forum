using Forum.Domain.Security;
using System.Collections.Generic;

namespace Forum.Domain.Models
{
    public class Subject : Entity<int>
    {
        public string Name { get; protected set; }

        public bool IsEdited { get; protected set; }

        public virtual Category Category { get; protected set; }

        public virtual ICollection<Topic> Topics { get; protected set; }

        protected Subject() { }

        public Subject(string name, Category category, User createdBy)
            : base(createdBy)
        {
            Name = name;
            Category = category;
        }

        public void Edit(string newName)
        {
            Name = newName;
            IsEdited = true;
        }

        public void ChangeCategory(Category newCategory)
        {
            Category = newCategory;
        }
    }
}