using Forum.Domain.Models;
using Forum.Framework.Infrastructure;

namespace Forum.Framework.Repositorires
{
    public interface IPostRepository : IRepository<int, Post>
    {
        // ^_^
    }
}
