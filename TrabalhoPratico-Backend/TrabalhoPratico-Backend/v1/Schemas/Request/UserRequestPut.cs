using System;
using static Entities.User;

namespace TrabalhoPratico_Backend.v1.Schemas.Request
{
    public class UserRequestPut
    {
        public Guid UserId { get; set; }
        public string? Name { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? Course { get; set; }
        public AccessType Role { get; set; } = AccessType.NULL;
    }
}

