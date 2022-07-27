using System;
using System.Collections.Generic;
using TrabalhoPratico_Backend;

namespace Entities
{
    public class Author : BaseEntity
    {
        private List<Book> _book = new();
        public IEnumerable<Book> Books => _book;

        public Author() { }

        public Author(string name)
        {
            Name = name;
        }
    }
}
