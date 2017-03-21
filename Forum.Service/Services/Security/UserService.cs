using Forum.Domain.Security;
using Forum.Framework.Infrastructure;
using Forum.Service.Contracts.Common;
using Forum.Service.Contracts.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq.Expressions;

namespace Forum.Service.Services.Security
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<int, User> repository;
        private readonly IImageService imageService;

        public UserService(IUnitOfWork unitOfWork, IRepository<int, User> repository, IImageService imageService)
        {
            this.unitOfWork = unitOfWork;
            this.repository = repository;
            this.imageService = imageService;
        }

        public void Create(User user)
        {
            repository.Create(user);
        }

        public void Update(User user)
        {
            repository.Update(user);
        }

        public User Get(int id)
        {
            return repository.Get(id);
        }

        public User Get(Expression<Func<User, bool>> where)
        {
            return repository.Get(where);
        }

        public IEnumerable<User> GetMany(Expression<Func<User, bool>> where)
        {
            return repository.GetMany(where);
        }

        public IEnumerable<User> GetAll()
        {
            return repository.GetAll();
        }

        public void SetRole(int id, Role role)
        {
            repository.Get(id).SetRole(role);
        }

        public void CommitChanges()
        {
            unitOfWork.Commit();
        }

        public void SetProfilePicture(User user, Image image)
        {
            var profilesImageAddress = ConfigurationManager.AppSettings["profileImageStorage"].ToString() + "\\" + user.Id;
            var uid = Guid.NewGuid();

            var profileImage32Address = profilesImageAddress + "\\" + uid.ToString() + "_32.jpg";
            var profileImage64Address = profilesImageAddress + "\\" + uid.ToString() + "_64.jpg";
            var profileImage128Address = profilesImageAddress + "\\" + uid.ToString() + "_128.jpg";

            if (!Directory.Exists(profilesImageAddress))
                Directory.CreateDirectory(profilesImageAddress);

            imageService.Resize(image, 32, 32).Save(profileImage32Address);
            imageService.Resize(image, 64, 64).Save(profileImage64Address);
            imageService.Resize(image, 128, 128).Save(profileImage128Address);

            user.UpdateProfile(profileImage32Address, profileImage64Address, profileImage128Address);
        }
    }
}
