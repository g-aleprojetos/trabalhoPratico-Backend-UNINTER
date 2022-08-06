using Context;
using Entities;
using System.Collections.Generic;
using System.Linq;
using TrabalhoPratico_Backend.Services.Interfaces;
using static Entities.User;

namespace TrabalhoPratico_Backend.Context
{
    public class SeedData
    {
        private Author author1;
        private Author author2;
        private Author author3;
        private Author author4;
        protected readonly ApiContext dbContext;

        public SeedData(ApiContext _dbContext) => dbContext = _dbContext;

        public void AplicarSeed()
        {
            if (!dbContext.Users.Any())
            {
                PopulateTestDataUsers();
            }

            if (!dbContext.Authors.Any())
            {
                PopulateTestDataAuthors();
            }

            if (!dbContext.Books.Any())
            {
                PopulateTestDataBooks();
            }
            dbContext.SaveChanges();
        }

        private void PopulateTestDataUsers()
        {
            var userAdm = new User("Administrador", "adm", "adm", "Usuario Adminitrativo", AccessType.ADM);
            var userAluno = new User("Alexandre", "Gonçalves", "3413992", "Tecnologia em Desenvolvimento de aplicativos Móveis", AccessType.USER);

            foreach (var item in dbContext.Users)
            {
                dbContext.Remove(item);
            }
            dbContext.Users.AddRange(userAdm, userAluno);


        }
        private void PopulateTestDataAuthors()
        {
            author1 = new Author("Luis Joyanes Aguilar");
            author2 = new Author("Simon Monk");
            author3 = new Author("Cláudio Luís Vieira Oliveira");
            author4 = new Author("Humberto Augusto Piovesana Zanetti");

            foreach (var item in dbContext.Authors)
            {
                dbContext.Remove(item);
            }

            dbContext.Authors.AddRange(author1, author2, author3, author4);


        }

        private void PopulateTestDataBooks()
        {
            List<Author> autores1 = new()
            {
                author1,
                author2
            };
            List<Author> autores2 = new() { author3 };
            List<Author> autores3 = new() { author4 };

            Book book1 = new("JAVASCRIPT DESCOMPLICADO - PROGRAMAÇÃO PARA WEB, IOT E DISPOSITIVOS MÓVEIS", "Érica", "Português", 217, 1, 2020, "Este Livro Apresenta Os Conceitos Fundamentais Que Possibilitam Aplicações Para A Web, Além De Ferramentas E Frameworks Mais Empregados, Incluindo O Uso De Sistemas De Bancos De Dados Para Realizar O Armazenamento Das Informações. Trata Das Mudanças Tecnológicas Atuais, Do Desenvolvimento De Soluções Para A Internet Das Coisas (IoT) E, Também, Do Uso Da Linguagem JavaScript No Desenvolvimento De Aplicativos Para Dispositivos Móveis.", autores1);
            Book book2 = new("Programação com Arduino II", "bookman", "Português", 258, 1, 2015, "Por Meio Deste Guia Prático E Ricamente Ilustrado, O Guru Da Eletrônica Simon Monk Leva Você Para Dentro Do Arduino E Revela Segredos Profissionais De Sua Programação. Com Ênfase Nas Placas De Arduino Uno, Leonardo E Due, Programação Com Arduino II: Passos Avançados Com Sketches Mostra Como Utilizar Interrupções, Gerenciar Memória, Fazer Programas Para A Internet, Maximizar As Comunicações Seriais, Realizar Processamento Digital De Sinal E Muito Mais!", autores2);
            Book book3 = new("Fundamentos de Programação", "3rd Edition", "Português", 706, 3, 2008, "Oferece Ferramentas Para Desenvolver Programas Eficientes E Bem-Estruturados, Que Servem De Base Para A Construção De Fundamentos Teóricos E Práticos De Programação. O Autor Utiliza Técnicas De Abstração Que Permitem Resolver Problemas De Programação De Modo Simples E Racional, Privilegiando A Aprendizagem Das Regras De Sintaxe E Solução De Problemas. O Livro Ensina A Programar Utilizando Conceitos Fundamentais. Para Isso, Descreve, Com Grande Quantidade De Exemplos E Exercícios, As Ferramentas De Programação Mais Utilizadas Na Aprendizagem Da Computação: Diagramas De Fluxo E Linguagem Algorítmica (Pseudocódigo).", autores3);

            foreach (var item in dbContext.Books)
            {
                dbContext.Remove(item);
            }
            dbContext.Books.AddRange(book1, book2, book3);

            







        }
    }
}
