using Forum.Domain.Security;
using Forum.Framework.Infrastructure;

namespace Forum.Framework.Repositorires
{
    public class UserRepository : RepositoryBase<int, User>, IUserRepository
    {
        public UserRepository(IDbDactory dbFactory)
            : base(dbFactory) { }
    }
}
