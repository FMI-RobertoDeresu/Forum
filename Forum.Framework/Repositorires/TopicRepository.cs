using Forum.Domain.Models;
using Forum.Framework.Infrastructure;

namespace Forum.Framework.Repositorires
{
    public class TopicRepository : RepositoryBase<int, Topic>, ITopicRepository
    {
        public TopicRepository(IDbDactory dbFactory)
            : base(dbFactory) { }
    }
}