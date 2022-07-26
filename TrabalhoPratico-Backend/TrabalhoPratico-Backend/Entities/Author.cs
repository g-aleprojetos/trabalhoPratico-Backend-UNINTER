using System;
using System.Collections.Generic;
using TrabalhoPratico_Backend;

namespace Entities
{
    public class Author : BaseEntity
    {
        public List<Book> Id_Book { get; set; }

    }
}
