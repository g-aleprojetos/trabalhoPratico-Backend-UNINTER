using System;
using System.Collections.Generic;

namespace TrabalhoPratico_Backend.v1.Schemas.Request
{
    public class BookRequestPut
    {
        public Guid BookId { get; set; }
        public string Name { get; set; } = null;
        public string Publisher { get; set; } = null;
        public string Language { get; set; } = null;
        public Nullable<int> Pages { get; set; } = null;
        public Nullable<int> Edition { get; set; } = null;
        public Nullable<int> Publication { get; set; } = null;
        public string Description { get; set; } = null;
        public IEnumerable<BookRequestAuthorPut> Authors { get; set; } = null;
    }

    public class BookRequestAuthorPut
    {
        public string Name { get; set; }

    }

}
