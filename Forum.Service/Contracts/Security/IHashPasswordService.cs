using Forum.Domain.Security;

namespace Forum.Service.Contracts.Security
{
    public interface IHashPasswordService
    {
        PasswordData GeneratePasswordHash(string password);
        bool ArePasswordsMatching(string password, PasswordData passwordData);
    }
}