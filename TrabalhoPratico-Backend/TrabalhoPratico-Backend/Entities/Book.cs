using System;
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

        private List<Author> _authors = new();
        public IEnumerable<Author> Authors => _authors;

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

    }
}
