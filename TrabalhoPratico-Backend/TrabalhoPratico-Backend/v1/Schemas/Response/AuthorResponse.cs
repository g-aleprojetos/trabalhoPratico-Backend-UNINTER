using Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Schemas.Response
{
    public class AuthorResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public static AuthorResponse Response(Author author) =>
            new(author.Id, author.Name);

        public AuthorResponse(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public class AuthorsResponse
    {
        public AuthorsResponse(IEnumerable<Author> author)
        {
            if (author == null) return;
            Authors = author
                .Select(AuthorResponse.Response)
                .OrderBy(element => element.Name);
        }
        public IEnumerable<AuthorResponse> Authors { get; set; }
    }
}
