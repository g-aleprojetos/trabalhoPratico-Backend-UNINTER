using Entities;
using System.Collections.Generic;

namespace TrabalhoPratico_Backend.v1.Schemas.Request
{
    //Cria o modelo de requisição do login
    public class LoginRequestPost
    {
        public string Login { get; set; }
        public string Password { get; set; }       
    }
}
