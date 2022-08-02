using System;

namespace TrabalhoPratico_Backend.v1.Controllers.Author
{
    public class AuthorRequestPut
    {
        public Guid AuthorId { get; set; }
        public string Name { get; set; }
    }
}
