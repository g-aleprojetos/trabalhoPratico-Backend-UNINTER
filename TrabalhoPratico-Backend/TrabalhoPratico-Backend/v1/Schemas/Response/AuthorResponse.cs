using Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Schemas.Response
{
    //Cria o modelo de resposta do Autor
    public class AuthorResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public static AuthorResponse Response(Author author) =>
            new(author.Id, author.Name);

        public AuthorResponse(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

    }
    //retorna lista de autores
    public class AuthorsResponse
    {
        public AuthorsResponse(IEnumerable<Author> author)
        {
            if (author == null) return;
            Authors = author
                .Select(AuthorResponse.Response)
                .OrderBy(element => element.Name);
        }
        public IEnumerable<AuthorResponse> Authors { get; set; }
    }
    //Retorna autor com livros
    public class ResponseAuthorWithBooks
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ResponseBookAuthor> Books { get; set; } = null;

        public static ResponseAuthorWithBooks ResponseAuthorWithBook(Author author) =>
            new(author.Id, author.Name, author.Books);

        public ResponseAuthorWithBooks(Guid id, string name, ICollection<Book> books)
        {
            Id = id;
            Name = name;
            Books = books.Select(ResponseBookAuthor.Response).OrderBy(element => element.Name);
        }
    }
    //retorna livro no autor
    public class ResponseBookAuthor
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string Language { get; set; }
        public int? Pages { get; set; }
        public int? Edition { get; set; }
        public int? Publication { get; set; }
        public string Description { get; set; }


        public static ResponseBookAuthor Response(Book book) =>
            new(book.Id, book.Name, book.Publisher, book.Language, book.Pages, book.Edition, book.Publication, book.Description);

        public ResponseBookAuthor(Guid id, string name, string publisher, string language, int? pages, int? edition, int? publication, string description)
        {
            Id = id;
            Name = name;
            Publisher = publisher;
            Language = language;
            Pages = pages;
            Edition = edition;
            Publication = publication;
            Description = description;
        }
    }
}
