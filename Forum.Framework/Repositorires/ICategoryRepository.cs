using Forum.Domain.Models;
using Forum.Framework.Infrastructure;

namespace Forum.Framework.Repositorires
{
    public interface ICategoryRepository : IRepository<int, Category>
    {
        // ^_^
    }
}
