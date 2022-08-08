using System.Collections.Generic;
using TrabalhoPratico_Backend;
using TrabalhoPratico_Backend.v1.Schemas.Request;

namespace Entities
{
    //A class Author usa a interface BaseEntity adquirindo Id, Name e Deletada
    public class Author : BaseEntity
    {
        //Propriedade da entidade 
        public ICollection<Book> Books { get; set; }

        public Author() { }
        //Contrutor para criar um autor
        public Author(string name)
        {
            Name = name;
        }
        //Método para atualizar um autor
        public void UpdateAuthor(AuthorRequest authorRequestPut)
        {
            if (authorRequestPut.Name != null) Name = authorRequestPut.Name;
        }
    }
}
