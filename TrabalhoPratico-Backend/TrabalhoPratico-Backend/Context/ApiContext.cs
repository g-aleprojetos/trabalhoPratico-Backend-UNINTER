using Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Context
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<User>()
                  .HasData(
                    new User() { Id = Guid.NewGuid(), Name = "Alexandre", Login = "Gonçalves", Password = "3413992", Course = "Tecnologia em Desenvolvimento de aplicativos Móveis" },
                    new User() { Id = Guid.NewGuid(), Name = "Administrador", Login = "adm", Password = "adm", Course = "Usuario Adminitrativo" });
            
            modelBuilder.Entity<Book>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Book>()
                  .HasData(
                    new Book() { Id = Guid.NewGuid(), Name = "JAVASCRIPT DESCOMPLICADO - PROGRAMAÇÃO PARA WEB, IOT E DISPOSITIVOS MÓVEIS",Publisher = "Érica",Language = "Português",Pages = 217,Edition = 1,Publication = 2020, Description = "Este Livro Apresenta Os Conceitos Fundamentais Que Possibilitam Aplicações Para A Web, Além De Ferramentas E Frameworks Mais Empregados, Incluindo O Uso De Sistemas De Bancos De Dados Para Realizar O Armazenamento Das Informações. Trata Das Mudanças Tecnológicas Atuais, Do Desenvolvimento De Soluções Para A Internet Das Coisas (IoT) E, Também, Do Uso Da Linguagem JavaScript No Desenvolvimento De Aplicativos Para Dispositivos Móveis." },
                    new Book() { Id = Guid.NewGuid(), Name = "Programação com Arduino II", Publisher = "bookman", Language = "Português", Pages = 258, Edition = 1,Publication = 2015, Description = "Por Meio Deste Guia Prático E Ricamente Ilustrado, O Guru Da Eletrônica Simon Monk Leva Você Para Dentro Do Arduino E Revela Segredos Profissionais De Sua Programação. Com Ênfase Nas Placas De Arduino Uno, Leonardo E Due, Programação Com Arduino II: Passos Avançados Com Sketches Mostra Como Utilizar Interrupções, Gerenciar Memória, Fazer Programas Para A Internet, Maximizar As Comunicações Seriais, Realizar Processamento Digital De Sinal E Muito Mais!" },
                    new Book() { Id = Guid.NewGuid(), Name = "Fundamentos de Programação", Publisher = "3rd Edition", Language = "Português",Pages = 706, Edition = 3, Publication = 2008,Description = "Oferece Ferramentas Para Desenvolver Programas Eficientes E Bem-Estruturados, Que Servem De Base Para A Construção De Fundamentos Teóricos E Práticos De Programação. O Autor Utiliza Técnicas De Abstração Que Permitem Resolver Problemas De Programação De Modo Simples E Racional, Privilegiando A Aprendizagem Das Regras De Sintaxe E Solução De Problemas. O Livro Ensina A Programar Utilizando Conceitos Fundamentais. Para Isso, Descreve, Com Grande Quantidade De Exemplos E Exercícios, As Ferramentas De Programação Mais Utilizadas Na Aprendizagem Da Computação: Diagramas De Fluxo E Linguagem Algorítmica (Pseudocódigo)." });

            modelBuilder.Entity<Author>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Author>()
                   .HasData(
                     new Author() { Id = Guid.NewGuid(), Name = "Luis Joyanes Aguilar" },
                     new Author() { Id = Guid.NewGuid(), Name = "Simon Monk"},
                     new Author() { Id = Guid.NewGuid(), Name = "Cláudio Luís Vieira Oliveira" },
                     new Author() { Id = Guid.NewGuid(), Name = "Humberto Augusto Piovesana Zanetti" });            
        }
    }
}
