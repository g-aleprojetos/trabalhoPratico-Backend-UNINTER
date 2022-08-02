using Entities;
using System;
using System.Collections.Generic;

namespace TrabalhoPratico_Backend.v1.Controllers.User
{
    public class UserRequestPut
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Course { get; set; }
    }
}

