using Forum.Domain.Security;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq.Expressions;

namespace Forum.Service.Contracts.Security
{
    public interface IUserService
    {
        void Create(User user);
        void Update(User user);

        User Get(int key);
        User Get(Expression<Func<User, bool>> where);
        IEnumerable<User> GetMany(Expression<Func<User, bool>> where);
        IEnumerable<User> GetAll();

        void SetRole(int id, Role role);
        void SetProfilePicture(User user, Image image);

        void CommitChanges();
    }
}
