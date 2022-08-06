using Ardalis.Specification;
using Entities;
using System;

namespace TrabalhoPratico_Backend.v1.Schemas.Specification
{
    public class UserWithBookEspecification : Specification<User>
    {
        public UserWithBookEspecification(Guid id)
        {
            Query.Where(u => u.Id == id && !u.Deletada)
                .Include(v => v.Books)
                .ThenInclude(x => x.Authors);
        }

        public UserWithBookEspecification()
        {
            Query.Where(u => !u.Deletada)
                .Include(v => v.Books)
                .ThenInclude(x => x.Authors);
        }
    }
}
