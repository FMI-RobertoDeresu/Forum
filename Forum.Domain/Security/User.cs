using Forum.Domain.Models;
using System;

namespace Forum.Domain.Security
{
    public class User : EntityBase<int>
    {
        public DateTime CreatedAt { get; protected set; }

        public string UserName { get; protected set; }

        public PasswordData Password { get; protected set; }

        public Role Role { get; protected set; }

        public string FirstName { get; protected set; }

        public string LastName { get; protected set; }

        public string FullName { get { return FirstName + " " + LastName; } }

        public string ProfileImage32Url { get; protected set; }

        public string ProfileImage64Url { get; protected set; }

        public string ProfileImage128Url { get; protected set; }

        protected User() { }

        public User(string userName, PasswordData password)
        {
            CreatedAt = DateTime.Now;
            UserName = userName;
            Password = password;

        }

        public void SetRole(Role role)
        {
            Role = role;
        }

        public void UpdateProfile(string firstName, string lastName, string profileImage32Url, string profileImage64Url,
            string profileImage128Url)
        {
            FirstName = firstName;
            LastName = lastName;
            ProfileImage32Url = profileImage32Url;
            ProfileImage64Url = profileImage64Url;
            ProfileImage128Url = profileImage128Url;
        }

        public void UpdateProfile(string profileImage32Url, string profileImage64Url, string profileImage128Url)
        {
            UpdateProfile(FirstName, LastName, profileImage32Url, profileImage64Url, profileImage128Url);
        }

        public void UpdateProfile(string firstName, string lastName)
        {
            UpdateProfile(firstName, lastName, ProfileImage32Url, ProfileImage64Url, ProfileImage128Url);
        }
    }
}