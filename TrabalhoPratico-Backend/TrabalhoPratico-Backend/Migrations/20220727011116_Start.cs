using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrabalhoPratico_Backend.Migrations
{
    public partial class Start : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deletada = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Publisher = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pages = table.Column<int>(type: "int", nullable: false),
                    Edition = table.Column<int>(type: "int", nullable: false),
                    Publication = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deletada = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Course = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deletada = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuthorBook",
                columns: table => new
                {
                    AuthorsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BooksId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorBook", x => new { x.AuthorsId, x.BooksId });
                    table.ForeignKey(
                        name: "FK_AuthorBook_Authors_AuthorsId",
                        column: x => x.AuthorsId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorBook_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Deletada", "Name" },
                values: new object[,]
                {
                    { new Guid("5cfe56a7-8628-4b0e-9a13-305c62814021"), false, "Luis Joyanes Aguilar" },
                    { new Guid("999e3ef9-6e11-43e3-89a8-0ccae97a8512"), false, "Simon Monk" },
                    { new Guid("39b0d98c-26ff-44ee-831d-7f8364837a7c"), false, "Cláudio Luís Vieira Oliveira" },
                    { new Guid("4dfaffa0-1d93-4684-a889-f81cba15a649"), false, "Humberto Augusto Piovesana Zanetti" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Deletada", "Description", "Edition", "Language", "Name", "Pages", "Publication", "Publisher" },
                values: new object[,]
                {
                    { new Guid("d9387582-816a-490a-9b3e-e921856f866c"), false, "Este Livro Apresenta Os Conceitos Fundamentais Que Possibilitam Aplicações Para A Web, Além De Ferramentas E Frameworks Mais Empregados, Incluindo O Uso De Sistemas De Bancos De Dados Para Realizar O Armazenamento Das Informações. Trata Das Mudanças Tecnológicas Atuais, Do Desenvolvimento De Soluções Para A Internet Das Coisas (IoT) E, Também, Do Uso Da Linguagem JavaScript No Desenvolvimento De Aplicativos Para Dispositivos Móveis.", 1, "Português", "JAVASCRIPT DESCOMPLICADO - PROGRAMAÇÃO PARA WEB, IOT E DISPOSITIVOS MÓVEIS", 217, 2020, "Érica" },
                    { new Guid("a0f04119-8f7d-4325-967a-4877a27017f1"), false, "Por Meio Deste Guia Prático E Ricamente Ilustrado, O Guru Da Eletrônica Simon Monk Leva Você Para Dentro Do Arduino E Revela Segredos Profissionais De Sua Programação. Com Ênfase Nas Placas De Arduino Uno, Leonardo E Due, Programação Com Arduino II: Passos Avançados Com Sketches Mostra Como Utilizar Interrupções, Gerenciar Memória, Fazer Programas Para A Internet, Maximizar As Comunicações Seriais, Realizar Processamento Digital De Sinal E Muito Mais!", 1, "Português", "Programação com Arduino II", 258, 2015, "bookman" },
                    { new Guid("fac0f58b-bf97-4541-b0d6-b4f557b1d03e"), false, "Oferece Ferramentas Para Desenvolver Programas Eficientes E Bem-Estruturados, Que Servem De Base Para A Construção De Fundamentos Teóricos E Práticos De Programação. O Autor Utiliza Técnicas De Abstração Que Permitem Resolver Problemas De Programação De Modo Simples E Racional, Privilegiando A Aprendizagem Das Regras De Sintaxe E Solução De Problemas. O Livro Ensina A Programar Utilizando Conceitos Fundamentais. Para Isso, Descreve, Com Grande Quantidade De Exemplos E Exercícios, As Ferramentas De Programação Mais Utilizadas Na Aprendizagem Da Computação: Diagramas De Fluxo E Linguagem Algorítmica (Pseudocódigo).", 3, "Português", "Fundamentos de Programação", 706, 2008, "3rd Edition" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Course", "Deletada", "Login", "Name", "Password" },
                values: new object[,]
                {
                    { new Guid("47005283-da6b-46ea-a51a-a6f3449b4a9c"), "Tecnologia em Desenvolvimento de aplicativos Móveis", false, "Gonçalves", "Alexandre", "3413992" },
                    { new Guid("f059212b-22d1-4572-9198-7a3895f0e4dd"), "Usuario Adminitrativo", false, "adm", "Administrador", "adm" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBook_BooksId",
                table: "AuthorBook",
                column: "BooksId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorBook");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
