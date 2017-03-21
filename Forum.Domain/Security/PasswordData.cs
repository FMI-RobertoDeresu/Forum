using System;

namespace Forum.Domain.Security
{
    public class PasswordData
    {
        public byte[] Hash { get; protected set; }

        public byte[] Salt { get; protected set; }

        public DateTime? ChangedAt { get; set; }

        protected PasswordData() { }

        public PasswordData(byte[] hash, byte[] salt)
        {
            Hash = hash;
            Salt = salt;
        }

        public void Change(byte[] hash, byte[] salt)
        {
            ChangedAt = DateTime.Now;
            Hash = hash;
            Salt = salt;
        }
    }
}