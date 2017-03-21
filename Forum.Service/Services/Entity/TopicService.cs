using Forum.Domain.Models;
using Forum.Framework.Infrastructure;
using Forum.Framework.Repositorires;
using Forum.Service.Contracts.Entity;

namespace Forum.Service.Services.Entity
{
    public class TopicService : EntityServiceBase<int, Topic>, ITopicService
    {
        public TopicService(IUnitOfWork unitOfWork, ITopicRepository repository)
            : base(unitOfWork, repository) { }
    }
}