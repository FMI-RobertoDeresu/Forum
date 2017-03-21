using Forum.Domain.Models;
using Forum.Framework.Infrastructure;

namespace Forum.Framework.Repositorires
{
    public class PostRepository : RepositoryBase<int, Post>, IPostRepository
    {
        public PostRepository(IDbDactory dbFactory)
            : base(dbFactory) { }
    }
}
