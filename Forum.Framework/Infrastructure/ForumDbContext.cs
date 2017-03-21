using Forum.Framework.Configuration;
using System.Data.Entity;

namespace Forum.Framework.Infrastructure
{
    public class ForumDbContext : DbContext
    {
        public ForumDbContext()
            : base("Default")
        {
            //Database.SetInitializer(new ForumDbInitializer());
        }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new CategoryConfiguration());
            modelBuilder.Configurations.Add(new SubjectConfiguration());
            modelBuilder.Configurations.Add(new TopicConfiguration());
            modelBuilder.Configurations.Add(new PostConfiguration());
        }
    }
}
