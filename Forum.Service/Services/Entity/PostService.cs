using Forum.Domain.Models;
using Forum.Framework.Infrastructure;
using Forum.Framework.Repositorires;
using Forum.Service.Contracts.Entity;

namespace Forum.Service.Services.Entity
{
    public class PostService : EntityServiceBase<int, Post>, IPostService
    {
        public PostService(IUnitOfWork unitOfWork, IPostRepository repository)
            : base(unitOfWork, repository) { }
    }
}