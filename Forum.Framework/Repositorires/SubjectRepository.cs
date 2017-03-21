using Forum.Domain.Models;
using Forum.Framework.Infrastructure;

namespace Forum.Framework.Repositorires
{
    public class SubjectRepository : RepositoryBase<int, Subject>, ISubjectRepository
    {
        public SubjectRepository(IDbDactory dbFactory)
            : base(dbFactory) { }
    }
}
