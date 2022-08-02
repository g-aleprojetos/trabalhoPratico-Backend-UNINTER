﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrabalhoPratico_Backend.Migrations
{
    public partial class start : Migration
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

            migrationBuilder.CreateTable(
                name: "BookUser",
                columns: table => new
                {
                    BooksId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookUser", x => new { x.BooksId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_BookUser_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Deletada", "Name" },
                values: new object[,]
                {
                    { new Guid("2e069fcf-adc1-4f83-90d9-d06ca7ed386b"), false, "Luis Joyanes Aguilar" },
                    { new Guid("9b17bc6a-95c2-4541-a9d6-341b4c57ac7d"), false, "Simon Monk" },
                    { new Guid("414789e3-412b-4fd8-b25b-0e4da5ce09a3"), false, "Cláudio Luís Vieira Oliveira" },
                    { new Guid("a8940a41-74e4-404a-80b7-107e90df378f"), false, "Humberto Augusto Piovesana Zanetti" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Deletada", "Description", "Edition", "Language", "Name", "Pages", "Publication", "Publisher" },
                values: new object[,]
                {
                    { new Guid("610044b9-b3f9-4ff6-b089-5ad1d7a626fb"), false, "Este Livro Apresenta Os Conceitos Fundamentais Que Possibilitam Aplicações Para A Web, Além De Ferramentas E Frameworks Mais Empregados, Incluindo O Uso De Sistemas De Bancos De Dados Para Realizar O Armazenamento Das Informações. Trata Das Mudanças Tecnológicas Atuais, Do Desenvolvimento De Soluções Para A Internet Das Coisas (IoT) E, Também, Do Uso Da Linguagem JavaScript No Desenvolvimento De Aplicativos Para Dispositivos Móveis.", 1, "Português", "JAVASCRIPT DESCOMPLICADO - PROGRAMAÇÃO PARA WEB, IOT E DISPOSITIVOS MÓVEIS", 217, 2020, "Érica" },
                    { new Guid("15e6ad78-f81b-4478-aa1c-347bfb85315f"), false, "Por Meio Deste Guia Prático E Ricamente Ilustrado, O Guru Da Eletrônica Simon Monk Leva Você Para Dentro Do Arduino E Revela Segredos Profissionais De Sua Programação. Com Ênfase Nas Placas De Arduino Uno, Leonardo E Due, Programação Com Arduino II: Passos Avançados Com Sketches Mostra Como Utilizar Interrupções, Gerenciar Memória, Fazer Programas Para A Internet, Maximizar As Comunicações Seriais, Realizar Processamento Digital De Sinal E Muito Mais!", 1, "Português", "Programação com Arduino II", 258, 2015, "bookman" },
                    { new Guid("0c949744-1024-46f3-a0ae-1390ab0c086c"), false, "Oferece Ferramentas Para Desenvolver Programas Eficientes E Bem-Estruturados, Que Servem De Base Para A Construção De Fundamentos Teóricos E Práticos De Programação. O Autor Utiliza Técnicas De Abstração Que Permitem Resolver Problemas De Programação De Modo Simples E Racional, Privilegiando A Aprendizagem Das Regras De Sintaxe E Solução De Problemas. O Livro Ensina A Programar Utilizando Conceitos Fundamentais. Para Isso, Descreve, Com Grande Quantidade De Exemplos E Exercícios, As Ferramentas De Programação Mais Utilizadas Na Aprendizagem Da Computação: Diagramas De Fluxo E Linguagem Algorítmica (Pseudocódigo).", 3, "Português", "Fundamentos de Programação", 706, 2008, "3rd Edition" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Course", "Deletada", "Login", "Name", "Password" },
                values: new object[,]
                {
                    { new Guid("a449db43-8f51-40a1-868f-be1d6885eb46"), "Tecnologia em Desenvolvimento de aplicativos Móveis", false, "Gonçalves", "Alexandre", "3413992" },
                    { new Guid("908561d3-6cce-46a5-a991-5ae0cc9b01c1"), "Usuario Adminitrativo", false, "adm", "Administrador", "adm" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBook_BooksId",
                table: "AuthorBook",
                column: "BooksId");

            migrationBuilder.CreateIndex(
                name: "IX_BookUser_UsersId",
                table: "BookUser",
                column: "UsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorBook");

            migrationBuilder.DropTable(
                name: "BookUser");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}