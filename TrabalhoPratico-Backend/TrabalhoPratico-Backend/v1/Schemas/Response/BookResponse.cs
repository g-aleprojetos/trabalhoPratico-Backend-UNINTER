using Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Schemas.Response
{
    public class BookResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<AuthorResponse> Authors { get; set; } = null;
        public string Publisher { get; set; }
        public string Language { get; set; }
        public Nullable<int> Pages { get; set; }
        public Nullable<int> Edition { get; set; }
        public Nullable<int> Publication { get; set; }
        public string Description { get; set; }

        public static BookResponse Response(Book book) =>
            new(book.Id, book.Name, book.Publisher, book.Language, book.Pages, book.Edition, book.Publication, book.Description, book.Authors);

 
        public BookResponse(Guid id, string name, string publisher, string language, int? pages, int? edition, int? publication, string description, IEnumerable<Author> authors)
        {
            Id = id;
            Name = name;
            Publisher = publisher;
            Language = language;
            Pages = pages;
            Edition = edition;
            Publication = publication;
            Description = description;
            Authors = authors == null ? null : authors.Select(AuthorResponse.Response).OrderBy(element => element.Name); 
        } 

    }


    public class BooksResponse
    {
        public BooksResponse(IEnumerable<Book> book)
        {
            Books = book
                .Select(BookResponse.Response)
                .OrderBy(element => element.Name);
        }
        public IEnumerable<BookResponse> Books { get; set; }
    }


}


