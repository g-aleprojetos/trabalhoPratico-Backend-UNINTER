using System;

namespace TrabalhoPratico_Backend.v1.Schemas.Request
{
    //Cria o modelo de requisição do put 
    public class AuthorRequest
    {
        public Guid AuthorId { get; set; }
        public string Name { get; set; }
    }
    //Cria o modelo de requisição do post
    public class AuthorRequestPost
    {
        public string Name { get; set; }
    }
}
