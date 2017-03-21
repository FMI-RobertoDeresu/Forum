namespace Forum.Framework.Infrastructure
{
    public interface IDbDactory
    {
        ForumDbContext Init();
    }
}
