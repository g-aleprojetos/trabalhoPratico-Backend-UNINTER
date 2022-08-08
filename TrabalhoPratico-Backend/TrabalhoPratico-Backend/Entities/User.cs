using System.Collections.Generic;
using TrabalhoPratico_Backend;
using TrabalhoPratico_Backend.Criptografia;
using TrabalhoPratico_Backend.v1.Schemas.Request;

namespace Entities
{
    //A class User usa a interface BaseEntity adquirindo Id, Name e Deletada
    public class User : BaseEntity
    {
        //Propriedade da entidade 
        public string Login { get; set; }
        public string Password { get; set; }
        public string Course { get; set; }
        public AccessType Role { get; set; }
        public string Token { get; set; }
        public ICollection<Book> Books { get; set; }

        public User() { }
        //Contrutor para criar um usuário
        public User(string name, string login, string password, string course, AccessType role)
        {
            Name = name;
            Login = login;
            Password = Encrypting(password);
            Course = course;
            Role = role;
        }
        //Método para atualizar um usuário
        public void UpdateUser(UserRequestPut userRequestPut)
        {
            if (userRequestPut.Name != null) Name = userRequestPut.Name;
            if (userRequestPut.Login != null) Login = userRequestPut.Login;
            if (userRequestPut.Password != null) Password = Encrypting(userRequestPut.Password);
            if (userRequestPut.Course != null) Course = userRequestPut.Course;
            if (userRequestPut.Role != AccessType.NULL) Role = userRequestPut.Role;
        }
        //Método que encripta a senha do usuário
        public string Encrypting(string valor)
        {
            var encryptedPassword = new Cryptography();
            return encryptedPassword.Encrypt(valor);
        }
        //Método para atualizar um livro na entidade usuário
        public void UpdateUserBook(ICollection<Book> book)
        {
            Books = book;
        }
        //Enum que classifica o tipo do usuário
        public enum AccessType
        {
            ADM,
            USER,
            NULL
        }
    }
}
