using Ardalis.Specification;
using Entities;
using System;

namespace TrabalhoPratico_Backend.v1.Schemas.Specification
{
    public class BooksEspecification : Specification<Book>
    {
        public BooksEspecification(Guid id)
        {
            Query.Where(u => u.Id == id && !u.Deletada)
                .Include(v => v.Authors);
        }

        public BooksEspecification()
        {
            Query.Where(u => !u.Deletada)
                .Include(v => v.Authors);
        }

    }
}
