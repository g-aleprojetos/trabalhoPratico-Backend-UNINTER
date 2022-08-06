using System;

namespace TrabalhoPratico_Backend
{
    public abstract class BaseEntity
    {
        public virtual Guid Id { get; set; }
        public string Name { get; set; }
        public bool Deletada { get; set; } = false;
    }
}
