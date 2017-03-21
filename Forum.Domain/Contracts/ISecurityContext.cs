using Forum.Domain.Security;

namespace Forum.Domain.Contracts
{
    public interface ISecurityContext
    {
        User User { get; }
        bool IsAuthenticated { get; }
        bool IsManager { get; }
        bool IsAdmin { get; }
        bool HasRole(string role);
    }
}
