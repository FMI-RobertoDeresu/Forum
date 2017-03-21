using Forum.Domain.Contracts;

namespace Forum.Framework
{
    public class ApplicationContext : IApplicationContext
    {
        private ISecurityContext _securityContext;

        public ApplicationContext(ISecurityContext securityContext)
        {
            _securityContext = securityContext;
        }

        public ISecurityContext SecurityContext
        {
            get { return _securityContext; }
        }
    }
}