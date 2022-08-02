using System.Collections.Generic;
using TrabalhoPratico_Backend;
using TrabalhoPratico_Backend.Criptografia;
using TrabalhoPratico_Backend.v1.Controllers.User;

namespace Entities
{
    public class User : BaseEntity
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Course { get; set; }
        public ICollection<Book> Books { get; set; }

        public User() { }

        public User(string name, string login, string password, string course)
        {
            Name = name;
            Login = login;
            Password = password;
            Course = course;
        }

        public User(string name, string login, string password, string course, Book books)
        {
            Name = name;
            Login = login;
            Password = password;
            Course = course;
            Books = (ICollection<Book>)books;
        }

        public void UpdateUser(UserRequestPut userRequestPut)
        {
            if (userRequestPut.Name != null) Name = userRequestPut.Name;
            if (userRequestPut.Login != null)Login = userRequestPut.Login;
            if (userRequestPut.Password != null)Password = Encrypting(userRequestPut.Password);
            if (userRequestPut.Course != null)Course = userRequestPut.Course;       
        }

        public string Encrypting(string valor)
        {
            var encryptedPassword = new Cryptography();
            return encryptedPassword.Encrypt(valor);
        }

   

    }
}
