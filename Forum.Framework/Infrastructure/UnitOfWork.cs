namespace Forum.Framework.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbDactory dbFactory;
        private ForumDbContext dbContext;

        public ForumDbContext DbContext
        {
            get { return dbContext ?? (dbContext = dbFactory.Init()); }
        }

        public UnitOfWork(IDbDactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public void Commit()
        {
            DbContext.Commit();
        }
    }
}
