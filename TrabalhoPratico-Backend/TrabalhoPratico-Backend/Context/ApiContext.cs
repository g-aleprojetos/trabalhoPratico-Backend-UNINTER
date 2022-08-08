using Entities;
using Microsoft.EntityFrameworkCore;

namespace Context
{
    public class ApiContext : DbContext
    {
        //Instancia o Contexto
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
        }
        //Entidades que é usado para criação das tabelas do banco de dados
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}
