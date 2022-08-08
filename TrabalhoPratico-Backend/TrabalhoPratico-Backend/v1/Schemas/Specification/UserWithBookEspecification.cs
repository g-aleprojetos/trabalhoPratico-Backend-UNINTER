using Ardalis.Specification;
using Entities;
using System;

namespace TrabalhoPratico_Backend.v1.Schemas.Specification
{
    public class UserWithBookEspecification : Specification<User>
    {
        //Monta a query de pesquisa de um livro com autores e usuario no banco
        public UserWithBookEspecification(Guid id)
        {
            Query.Where(u => u.Id == id && !u.Deletada)
                .Include(v => v.Books)
                .ThenInclude(x => x.Authors);
        }
        //Monta a query de pesquisa de livros com autores e usuarios no banco
        public UserWithBookEspecification()
        {
            Query.Where(u => !u.Deletada)
                .Include(v => v.Books)
                .ThenInclude(x => x.Authors);
        }
    }
}
