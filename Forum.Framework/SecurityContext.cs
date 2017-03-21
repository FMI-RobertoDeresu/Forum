using Forum.Domain.Contracts;
using Forum.Domain.Security;
using System;
using System.Linq;
using System.Web;

namespace Forum.Framework
{
    public class SecurityContext : ISecurityContext
    {
        private readonly User user;

        public SecurityContext(User user)
        {
            this.user = user;
        }

        public static ISecurityContext Current
        {
            get
            {
                return HttpContext.Current.Items[Environment.SecurityContextKey] as ISecurityContext;
            }
        }

        public User User
        {
            get
            {
                return user;
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return user != null;
            }
        }

        public bool IsManager
        {
            get
            {
                return IsAuthenticated && user.Role == Role.Manager;
            }
        }

        public bool IsAdmin
        {
            get
            {
                return IsAuthenticated && user.Role == Role.Admin;
            }
        }

        public bool HasRole(string role)
        {
            return User.Role.ToString().ToUpper() == role.ToUpper();
        }
    }
}