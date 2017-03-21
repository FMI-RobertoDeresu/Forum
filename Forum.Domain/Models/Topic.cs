using Forum.Domain.Security;
using System.Collections.Generic;

namespace Forum.Domain.Models
{
    public class Topic : Entity<int>
    {
        public string Name { get; protected set; }

        public bool IsEdited { get; protected set; }

        public virtual Subject Subject { get; protected set; }

        public virtual ICollection<Post> Posts { get; protected set; }

        protected Topic() { }

        public Topic(string name, Subject subject, User createdBy)
            : base(createdBy)
        {
            Name = name;
            Subject = subject;
        }

        public void Edit(string newName)
        {
            Name = newName;
            IsEdited = true;
        }

        public void ChangeSubject(Subject newSubject)
        {
            Subject = newSubject;
        }
    }
}
