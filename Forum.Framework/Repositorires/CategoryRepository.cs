using Forum.Domain.Models;
using Forum.Framework.Infrastructure;

namespace Forum.Framework.Repositorires
{
    public class CategoryRepository : RepositoryBase<int, Category>, ICategoryRepository
    {
        public CategoryRepository(IDbDactory dbFactory)
            : base(dbFactory) { }
    }
}
