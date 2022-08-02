using Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Schemas.Response
{
    public class UserResponse
    {
        private int pages;
        private int edition;
        private int publication;
        private string description;
        private ICollection<Author> authors;

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Course { get; set; }
        public ICollection<Book> Books { get; set; }

        public static UserResponse Response(User user) =>
            new(user.Id, user.Name, user.Login, user.Course);

        public UserResponse(Guid id, string name, string login, string course, Book books)
        {
            Id = id;
            Name = name;
            Login = login;
            Course = course;
            Books = (ICollection<Book>)books;
        }

        public UserResponse(Guid id, string name, string login, string course)
        {
            Id = id;
            Name = name;
            Login = login;
            Course = course;
        }

    }

    public class UsersResponse
    {
        public UsersResponse(IEnumerable<User> user)
        {
            Users = user
                .Select(UserResponse.Response)
                .OrderBy(element => element.Name);
        }
        public IEnumerable<UserResponse> Users { get; set; }
    }
}