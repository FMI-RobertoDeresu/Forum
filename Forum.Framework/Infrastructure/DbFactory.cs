namespace Forum.Framework.Infrastructure
{
    public class DbFactory : Disposable, IDbDactory
    {
        private ForumDbContext dbContext;

        public ForumDbContext Init()
        {
            return dbContext ?? (dbContext = new ForumDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
