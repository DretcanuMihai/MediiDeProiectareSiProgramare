using System;

namespace model.entities
{
    [Serializable]
    public class User : Entity<string>
    {
        public string Password { get; set; }

        public string Username
        {
            get { return Id; }
        }

        public User(string username, string password) : base(username)
        {
            Password = password;
        }
    }
}