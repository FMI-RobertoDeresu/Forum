using Forum.Domain.Security;

namespace Forum.Framework.Configuration
{
    internal class UserConfiguration : EntityBaseConfiguration<int, User>
    {
        public UserConfiguration()
        {
            Property(x => x.CreatedAt).IsRequired();
            Property(x => x.UserName).IsRequired();
            Property(x => x.Password.ChangedAt);
            Property(x => x.Password.Hash).IsRequired();
            Property(x => x.Password.Salt).IsRequired();
            Property(x => x.Role).IsRequired();
            Property(x => x.FirstName);
            Property(x => x.LastName);
            Property(x => x.ProfileImage32Url);
            Property(x => x.ProfileImage64Url);
            Property(x => x.ProfileImage128Url);
        }
    }
}