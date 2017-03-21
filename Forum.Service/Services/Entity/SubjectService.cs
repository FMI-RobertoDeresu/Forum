using Forum.Domain.Models;
using Forum.Framework.Infrastructure;
using Forum.Framework.Repositorires;
using Forum.Service.Contracts.Entity;

namespace Forum.Service.Services.Entity
{
    public class SubjectService : EntityServiceBase<int, Subject>, ISubjectService
    {
        public SubjectService(IUnitOfWork unitOfWork, ISubjectRepository subjectRepository)
            : base(unitOfWork, subjectRepository) { }
    }
}