using Forum.Domain.Security;

namespace Forum.Domain.Models
{
    public class Post : Entity<int>
    {
        public string Text { get; protected set; }

        public bool IsEdited { get; protected set; }

        public virtual Topic Topic { get; protected set; }

        protected Post() { }

        public Post(string text, Topic topic, User createdBy)
            : base(createdBy)
        {
            Text = text;
            Topic = topic;
        }

        public void Edit(string newText)
        {
            Text = newText;
            IsEdited = true;
        }
    }
}