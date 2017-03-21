using Forum.Domain.Models;

namespace Forum.Framework.Configuration
{
    internal class PostConfiguration : EntityConfiguration<int, Post>
    {
        public PostConfiguration()
        {
            Property(x => x.Text).IsRequired();
            Property(x => x.IsEdited).IsRequired();
            HasRequired(x => x.Topic);
        }
    }
}
