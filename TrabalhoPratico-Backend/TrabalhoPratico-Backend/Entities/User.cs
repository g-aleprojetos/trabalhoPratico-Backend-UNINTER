using System;

namespace Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Password { get; set; }
        public string Course { get; set; }

    }
}
