using Forum.Domain.Models;

namespace Forum.Framework.Configuration
{
    internal class SubjectConfiguration : EntityConfiguration<int, Subject>
    {
        public SubjectConfiguration()
        {
            Property(x => x.Name).IsRequired();
            Property(x => x.IsEdited).IsRequired();
            HasRequired(x => x.Category).WithMany(x => x.Subjects);
            HasMany(x => x.Topics).WithRequired(x => x.Subject).WillCascadeOnDelete(true);
        }
    }
}