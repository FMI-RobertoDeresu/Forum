using Forum.Domain.Models;

namespace Forum.Framework.Configuration
{
    internal class TopicConfiguration : EntityConfiguration<int, Topic>
    {
        public TopicConfiguration()
        {
            Property(x => x.Name).IsRequired();
            Property(x => x.IsEdited).IsRequired();
            HasRequired(x => x.Subject);
            HasMany(x => x.Posts).WithRequired(x => x.Topic).WillCascadeOnDelete(true);
        }
    }
}
