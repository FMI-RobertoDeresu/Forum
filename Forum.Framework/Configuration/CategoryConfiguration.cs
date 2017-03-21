using Forum.Domain.Models;

namespace Forum.Framework.Configuration
{
    internal class CategoryConfiguration : EntityConfiguration<int, Category>
    {
        public CategoryConfiguration()
        {
            Property(x => x.Name).IsRequired();
            Property(x => x.IsEdited).IsRequired();
            HasMany(x => x.Subjects).WithRequired(x => x.Category).WillCascadeOnDelete(true);
        }
    }
}