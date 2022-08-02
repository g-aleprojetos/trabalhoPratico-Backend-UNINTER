using System.Collections.Generic;
using TrabalhoPratico_Backend;

namespace Entities
{
    public class Book : BaseEntity
    {
        public string Publisher { get; set; }
        public string Language { get; set; }
        public int Pages { get; set; }
        public int Edition { get; set; }
        public int Publication { get; set; }
        public string Description { get; set; }
        public ICollection<User> Users { get; set; }
        public IEnumerable<Author> Authors { get; set; }

        public Book() { }

        public Book(string name, string publisher, string language, int pages, int edition, int publication, string description)
        {
            Name = name;
            Publisher = publisher;
            Language = language;
            Pages = pages;
            Edition = edition;
            Publication = publication;
            Description = description;
        }

        public Book(string name, string publisher, string language, int pages, int edition, int publication, string description, ICollection<Author> authors)
        {
            Name = name;
            Publisher = publisher;
            Language = language;
            Pages = pages;
            Edition = edition;
            Publication = publication;
            Description = description;
            Authors = authors;
        }
    }
}
