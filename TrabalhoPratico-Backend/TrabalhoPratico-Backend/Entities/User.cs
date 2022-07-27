using System;
using TrabalhoPratico_Backend;

namespace Entities
{
    public class User : BaseEntity
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Course { get; set; }

        public User() { }

        public User(string name,string login, string password, string course)
        {
            Name = name;
            Login = login;
            Password = password;
            Course = course;
        }

    }
}
