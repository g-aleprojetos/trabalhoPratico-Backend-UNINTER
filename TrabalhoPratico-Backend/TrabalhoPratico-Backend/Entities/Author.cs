using System.Collections.Generic;
using TrabalhoPratico_Backend;
using TrabalhoPratico_Backend.v1.Schemas.Request;

namespace Entities
{
    public class Author : BaseEntity
    {
        public ICollection<Book> Books { get; set; }

        public Author() { }
        public Author(string name)
        {
            Name = name;
        }

        public void UpdateAuthor(AuthorRequestPut authorRequestPut)
        {
            if (authorRequestPut.Name != null) Name = authorRequestPut.Name;
        }

    }
}
