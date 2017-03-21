using System.Data.Entity;

namespace Forum.Framework.Infrastructure
{
    internal class ForumDbInitializer : CreateDatabaseIfNotExists<ForumDbContext>
    {
        protected override void Seed(ForumDbContext context)
        {
            base.Seed(context);
        }
    }
}
