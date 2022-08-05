using Context;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace TrabalhoPratico_Backend.Context
{
    public class SeedData
    {
        public static readonly User userAdm = new("Administrador", "adm", "adm", "Usuario Adminitrativo");
        public static readonly User userAluno = new("Alexandre", "Gonçalves", "3413992", "Tecnologia em Desenvolvimento de aplicativos Móveis");

        public static readonly Author author1 = new("Luis Joyanes Aguilar");
        public static readonly Author author2 = new("Simon Monk");
        public static readonly Author author3 = new("Cláudio Luís Vieira Oliveira");
        public static readonly Author author4 = new("Humberto Augusto Piovesana Zanetti");

        public static readonly Book book1 = new("JAVASCRIPT DESCOMPLICADO - PROGRAMAÇÃO PARA WEB, IOT E DISPOSITIVOS MÓVEIS", "Érica", "Português", 217, 1, 2020, "Este Livro Apresenta Os Conceitos Fundamentais Que Possibilitam Aplicações Para A Web, Além De Ferramentas E Frameworks Mais Empregados, Incluindo O Uso De Sistemas De Bancos De Dados Para Realizar O Armazenamento Das Informações. Trata Das Mudanças Tecnológicas Atuais, Do Desenvolvimento De Soluções Para A Internet Das Coisas (IoT) E, Também, Do Uso Da Linguagem JavaScript No Desenvolvimento De Aplicativos Para Dispositivos Móveis.");
        public static readonly Book book2 = new("Programação com Arduino II", "bookman", "Português", 258, 1, 2015, "Por Meio Deste Guia Prático E Ricamente Ilustrado, O Guru Da Eletrônica Simon Monk Leva Você Para Dentro Do Arduino E Revela Segredos Profissionais De Sua Programação. Com Ênfase Nas Placas De Arduino Uno, Leonardo E Due, Programação Com Arduino II: Passos Avançados Com Sketches Mostra Como Utilizar Interrupções, Gerenciar Memória, Fazer Programas Para A Internet, Maximizar As Comunicações Seriais, Realizar Processamento Digital De Sinal E Muito Mais!");
        public static readonly Book book3 = new("Fundamentos de Programação", "3rd Edition", "Português", 706, 3, 2008, "Oferece Ferramentas Para Desenvolver Programas Eficientes E Bem-Estruturados, Que Servem De Base Para A Construção De Fundamentos Teóricos E Práticos De Programação. O Autor Utiliza Técnicas De Abstração Que Permitem Resolver Problemas De Programação De Modo Simples E Racional, Privilegiando A Aprendizagem Das Regras De Sintaxe E Solução De Problemas. O Livro Ensina A Programar Utilizando Conceitos Fundamentais. Para Isso, Descreve, Com Grande Quantidade De Exemplos E Exercícios, As Ferramentas De Programação Mais Utilizadas Na Aprendizagem Da Computação: Diagramas De Fluxo E Linguagem Algorítmica (Pseudocódigo).");


        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var dbContext = new ApiContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApiContext>>());
            if (!dbContext.Users.Any())
            {
                PopulateTestDataUsers(dbContext);
            }

            if (!dbContext.Authors.Any())
            {
                PopulateTestDataAuthors(dbContext);
            }

            if (!dbContext.Books.Any())
            {
                PopulateTestDataBooks(dbContext);
            }

            return;
        }

        private static void PopulateTestDataUsers(ApiContext dbApiContext)
        {
            foreach (var item in dbApiContext.Users)
            {
                dbApiContext.Remove(item);
            }
            dbApiContext.SaveChanges();
            dbApiContext.Users.Add(userAdm);
            dbApiContext.Users.Add(userAluno);

            dbApiContext.SaveChanges();
        }
        private static void PopulateTestDataAuthors(ApiContext dbApiContext)
        {
            foreach (var item in dbApiContext.Authors)
            {
                dbApiContext.Remove(item);
            }
            dbApiContext.SaveChanges();
            dbApiContext.Authors.Add(author1);
            dbApiContext.Authors.Add(author2);
            dbApiContext.Authors.Add(author3);
            dbApiContext.Authors.Add(author4);

            dbApiContext.SaveChanges();
        }

        private static void PopulateTestDataBooks(ApiContext dbApiContext)
        {
            foreach (var item in dbApiContext.Books)
            {
                dbApiContext.Remove(item);
            }
            dbApiContext.SaveChanges();
            dbApiContext.Books.Add(book1);
            dbApiContext.Books.Add(book2);
            dbApiContext.Books.Add(book3);

            dbApiContext.SaveChanges();
        }
    }
}
