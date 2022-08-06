using System;

namespace TrabalhoPratico_Backend.v1.Schemas.Request
{
    public class AuthorRequest
    {
        public Guid AuthorId { get; set; }
        public string Name { get; set; }
    }

    public class AuthorRequestPost
    {
        public string Name { get; set; }
    }
}
