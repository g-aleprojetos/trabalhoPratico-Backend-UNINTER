using System;
using System.Collections.Generic;

namespace TrabalhoPratico_Backend.v1.Schemas.Request
{
    //Cria o modelo de requisição do post e Put
    public class UserWithBookRequest
    {
        public Guid UserId { get; set; }
        public ICollection<BookRequestUser>? Books { get; set; } 
    }

    public class BookRequestUser
    {
        public Guid BookId { get; set; }

    }
}
