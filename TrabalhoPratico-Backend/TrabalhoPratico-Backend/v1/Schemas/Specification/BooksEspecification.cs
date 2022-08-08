using Ardalis.Specification;
using Entities;
using System;

namespace TrabalhoPratico_Backend.v1.Schemas.Specification
{
    public class BooksEspecification : Specification<Book>
    {
        //Monta a query de pesquisa de um livro com autores no banco
        public BooksEspecification(Guid id)
        {
            Query.Where(u => u.Id == id && !u.Deletada)
                .Include(v => v.Authors);
        }
        //Monta a query de pesquisa de livros com autores no banco
        public BooksEspecification()
        {
            Query.Where(u => !u.Deletada)
                .Include(v => v.Authors);
        }
    }
}
