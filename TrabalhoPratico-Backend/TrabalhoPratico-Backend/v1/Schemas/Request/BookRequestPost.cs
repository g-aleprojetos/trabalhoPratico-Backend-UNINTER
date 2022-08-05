using System;
using System.Collections.Generic;

namespace TrabalhoPratico_Backend.v1.Schemas.Request
{
    public class BookRequestPost
    {
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string Language { get; set; }
        public int Pages { get; set; }
        public int Edition { get; set; }
        public int Publication { get; set; }
        public string Description { get; set; }
        public ICollection<BookRequestAuthorPost> Authors { get; set; }
    }

    public class BookRequestAuthorPost
    {
        public string Name { get; set; }

    }

}
