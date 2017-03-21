using Forum.Domain.Security;
using Forum.Service.Contracts.Security;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Forum.Service.Services.Security
{
    public class HashPasswordService : IHashPasswordService
    {
        public PasswordData GeneratePasswordHash(string password)
        {
            using (var algorithm = new SHA512Managed())
            {
                byte[] salt = Guid.NewGuid().ToByteArray();
                byte[] passHash = algorithm.ComputeHash(ByteArrayConcat(Encoding.Unicode.GetBytes(password), salt));

                return new PasswordData(passHash, salt);
            }
        }

        public bool ArePasswordsMatching(string password, PasswordData passwordData)
        {
            using (var algorithm = new SHA512Managed())
            {
                byte[] passHash = algorithm.ComputeHash(ByteArrayConcat(Encoding.Unicode.GetBytes(password), passwordData.Salt));

                return passHash.SequenceEqual(passwordData.Hash);
            }
        }

        private byte[] ByteArrayConcat(byte[] first, byte[] second)
        {
            byte[] result = new byte[first.Length + second.Length];

            Buffer.BlockCopy(first, 0, result, 0, first.Length);
            Buffer.BlockCopy(second, 0, result, first.Length, second.Length);

            return result;
        }
    }
}
