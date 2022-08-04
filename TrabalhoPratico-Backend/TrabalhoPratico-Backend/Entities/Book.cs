using Schemas.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using TrabalhoPratico_Backend;
using TrabalhoPratico_Backend.v1.Schemas.Request;

namespace Entities
{
    public class Book : BaseEntity
    {
        public string Publisher { get; set; }
        public string Language { get; set; }
        public int? Pages { get; set; }
        public int? Edition { get; set; }
        public int? Publication { get; set; }
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

        public void UpdateBook(string name, string publisher, string language, int? pages, int? edition, int? publication, string description, ICollection<Author> authors)
        {
            if (name != null) Name = name;
            if (publisher != null) Publisher = publisher;
            if (language != null) Language = language;
            if(pages != null) Pages = pages;
            if(edition != null) Edition = edition;
            if(publication != null) Publication = publication;
            if (description != null) Description = description;
            if (authors.Count != 0) Authors = authors;
        }
    }
}
