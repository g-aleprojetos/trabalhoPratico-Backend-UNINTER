using System;
using System.Collections.Generic;
using TrabalhoPratico_Backend;
using TrabalhoPratico_Backend.v1.Controllers.Author;

namespace Entities
{
    public class Author : BaseEntity
    {
        public ICollection<Book> Books { get; set; }

        public Author(){ }
        public Author(string name) 
        {
            Name = name;
        }

        public void UpdateAuthor(AuthorRequestPut authorRequestPut)
        {
            if (authorRequestPut.Name != null) Name = authorRequestPut.Name;
        }

        public Author AddAuthor(string name)
        {
           
            return this;
        }
    }
}
