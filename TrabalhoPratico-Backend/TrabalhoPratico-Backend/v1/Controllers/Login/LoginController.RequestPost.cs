using Entities;
using System.Collections.Generic;

namespace TrabalhoPratico_Backend.v1.Controllers.User
{
    public class UserRequestPost
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Course { get; set; }       
    }
}
