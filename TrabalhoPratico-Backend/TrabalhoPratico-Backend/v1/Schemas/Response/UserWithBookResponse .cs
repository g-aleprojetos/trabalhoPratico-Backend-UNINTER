using Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Schemas.Response
{
    //Cria o modelo de resposta do Usuário com Livro
    public class ResponseUserWithBooks
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<BookResponse> Books { get; set; }

        public static ResponseUserWithBooks ResponseAuthorWithBook(User user) =>
            new(user.Id, user.Name, user.Books);

        public ResponseUserWithBooks(Guid id, string name, ICollection<Book> books)
        {
            Id = id;
            Name = name;
            Books = books.Select(BookResponse.Response).OrderBy(element => element.Name);
        }
    }
    //Retorna lista deusuários com livros
    public class UsersWithBookResponse
    {
        public UsersWithBookResponse(IEnumerable<User> user)
        {
            Users = user
                .Select(ResponseUserWithBooks.ResponseAuthorWithBook)
                .OrderBy(element => element.Name);
        }
        public IEnumerable<ResponseUserWithBooks> Users { get; set; }
    }
}