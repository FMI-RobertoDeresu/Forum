using System.Data.Entity.Migrations;

namespace Forum.Framework.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Infrastructure.ForumDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Forum.Framework.Infrastructure.ForumDbContext";
        }
    }
}
