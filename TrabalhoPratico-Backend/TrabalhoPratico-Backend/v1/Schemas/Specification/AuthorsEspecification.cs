using Ardalis.Specification;
using Entities;
using System;

namespace TrabalhoPratico_Backend.v1.Schemas.Specification
{
    public class AuthorsEspecification : Specification<Author>
    {
        //Monta a query de pesquisa de um Autor com livros no banco
        public AuthorsEspecification(Guid id)
        {
            Query.Where(u => u.Id == id && !u.Deletada)
                .Include(v => v.Books);
        }
    }
}
