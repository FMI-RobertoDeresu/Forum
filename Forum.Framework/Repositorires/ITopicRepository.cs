using Forum.Domain.Models;
using Forum.Framework.Infrastructure;

namespace Forum.Framework.Repositorires
{
    public interface ITopicRepository : IRepository<int, Topic>
    {
        // ^_^
    }
}
