using System;
using System.Collections.Generic;
using TrabalhoPratico_Backend;

namespace Entities
{
    public class Book : BaseEntity
    {
        public List<Author> Id_Author { get; set; }
        public string Publisher { get; set; }
        public string Language { get; set; }
        public int Pages { get; set; }
        public int Edition { get; set; }
        public int Publication { get; set; }
        public string Description { get; set; }
    }
}
