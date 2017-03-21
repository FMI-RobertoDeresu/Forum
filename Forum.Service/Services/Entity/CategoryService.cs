using Forum.Domain.Models;
using Forum.Framework.Infrastructure;
using Forum.Framework.Repositorires;
using Forum.Service.Contracts.Entity;

namespace Forum.Service.Services.Entity
{
    public class CategoryService : EntityServiceBase<int, Category>, ICategoryService
    {
        public CategoryService(IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
            : base(unitOfWork, categoryRepository) { }
    }
}