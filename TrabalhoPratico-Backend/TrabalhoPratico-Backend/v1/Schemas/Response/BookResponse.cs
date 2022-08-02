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
        public ICollection<string> Authors { get; set; }
        public string Publisher { get; set; }
        public string Language { get; set; }
        public int Pages { get; set; }
        public int Edition { get; set; }
        public int Publication { get; set; }
        public string Description { get; set; }

        public static BookResponse Response(Book book) =>
            new(book.Id, book.Name, book.Publisher, book.Language, book.Pages, book.Edition, book.Publication, book.Description, book.Authors);

 
        public BookResponse(Guid id, string name, string publisher, string language, int pages, int edition, int publication, string description, ICollection<Author> authors)
        {
            Id = id;
            Name = name;
            Publisher = publisher;
            Language = language;
            Pages = pages;
            Edition = edition;
            Publication = publication;
            Description = description;
            List<string>names = new();

            foreach (var author in authors)
            {
                names.Add(author.Name.ToString());
            }
            Authors = names;
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




//public async Task<Batalha> GetBatalhaById(int id, bool incluirHerois = false)
//{
//    IQueryable<Batalha> query = _context.Batalhas;

//    if (incluirHerois)
//    {
//        query = query.Include(h => h.HeroisBatalhas)
//                 .ThenInclude(heroib => heroib.Heroi);
//    }

//    query = query.AsNoTracking().OrderBy(h => h.Id);

//    return await query.FirstOrDefaultAsync();
//}
