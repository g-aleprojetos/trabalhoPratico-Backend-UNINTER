using System;
using System.Collections.Generic;

namespace Entities
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Author> Author { get; set; }
        public string Publisher { get; set; }
        public string Language { get; set; }
        public int Pages { get; set; }
        public int Edition { get; set; }
        public int Publication { get; set; }
        public string Description { get; set; }
    }
}
