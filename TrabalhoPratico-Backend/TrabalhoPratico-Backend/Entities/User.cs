using System;
using TrabalhoPratico_Backend;

namespace Entities
{
    public class User : BaseEntity
    {
        public string LastName { get; set; }
        public int Password { get; set; }
        public string Course { get; set; }

    }
}
