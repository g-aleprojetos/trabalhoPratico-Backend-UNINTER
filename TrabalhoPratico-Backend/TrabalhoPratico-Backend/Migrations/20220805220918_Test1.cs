using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrabalhoPratico_Backend.Migrations
{
    public partial class Test1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("17f1f611-7152-4d7f-8f58-9ee482453dee"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("28c7bdcb-e444-498c-9fae-5b27af05fc6d"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("4ecf831d-9c8a-4e0e-935b-d815d2c023f1"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("6f324067-0ef2-4836-a3e7-36ef41446f26"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("7016e77c-ec02-45b4-b7a0-8db751749bad"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("ad87fbe6-6fb1-4239-becb-be5c7da0b2f0"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("e3429a75-e2c9-470c-bfad-4d48708e0920"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("275873ff-cc28-44d6-b845-7678af64ed50"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bb55880d-9b42-4388-a688-1b7a3625467c"));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Books",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Authors",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWID()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Books",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Authors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Deletada", "Name" },
                values: new object[,]
                {
                    { new Guid("28c7bdcb-e444-498c-9fae-5b27af05fc6d"), false, "Luis Joyanes Aguilar" },
                    { new Guid("6f324067-0ef2-4836-a3e7-36ef41446f26"), false, "Simon Monk" },
                    { new Guid("4ecf831d-9c8a-4e0e-935b-d815d2c023f1"), false, "Cláudio Luís Vieira Oliveira" },
                    { new Guid("17f1f611-7152-4d7f-8f58-9ee482453dee"), false, "Humberto Augusto Piovesana Zanetti" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Deletada", "Description", "Edition", "Language", "Name", "Pages", "Publication", "Publisher" },
                values: new object[,]
                {
                    { new Guid("e3429a75-e2c9-470c-bfad-4d48708e0920"), false, "Este Livro Apresenta Os Conceitos Fundamentais Que Possibilitam Aplicações Para A Web, Além De Ferramentas E Frameworks Mais Empregados, Incluindo O Uso De Sistemas De Bancos De Dados Para Realizar O Armazenamento Das Informações. Trata Das Mudanças Tecnológicas Atuais, Do Desenvolvimento De Soluções Para A Internet Das Coisas (IoT) E, Também, Do Uso Da Linguagem JavaScript No Desenvolvimento De Aplicativos Para Dispositivos Móveis.", 1, "Português", "JAVASCRIPT DESCOMPLICADO - PROGRAMAÇÃO PARA WEB, IOT E DISPOSITIVOS MÓVEIS", 217, 2020, "Érica" },
                    { new Guid("7016e77c-ec02-45b4-b7a0-8db751749bad"), false, "Por Meio Deste Guia Prático E Ricamente Ilustrado, O Guru Da Eletrônica Simon Monk Leva Você Para Dentro Do Arduino E Revela Segredos Profissionais De Sua Programação. Com Ênfase Nas Placas De Arduino Uno, Leonardo E Due, Programação Com Arduino II: Passos Avançados Com Sketches Mostra Como Utilizar Interrupções, Gerenciar Memória, Fazer Programas Para A Internet, Maximizar As Comunicações Seriais, Realizar Processamento Digital De Sinal E Muito Mais!", 1, "Português", "Programação com Arduino II", 258, 2015, "bookman" },
                    { new Guid("ad87fbe6-6fb1-4239-becb-be5c7da0b2f0"), false, "Oferece Ferramentas Para Desenvolver Programas Eficientes E Bem-Estruturados, Que Servem De Base Para A Construção De Fundamentos Teóricos E Práticos De Programação. O Autor Utiliza Técnicas De Abstração Que Permitem Resolver Problemas De Programação De Modo Simples E Racional, Privilegiando A Aprendizagem Das Regras De Sintaxe E Solução De Problemas. O Livro Ensina A Programar Utilizando Conceitos Fundamentais. Para Isso, Descreve, Com Grande Quantidade De Exemplos E Exercícios, As Ferramentas De Programação Mais Utilizadas Na Aprendizagem Da Computação: Diagramas De Fluxo E Linguagem Algorítmica (Pseudocódigo).", 3, "Português", "Fundamentos de Programação", 706, 2008, "3rd Edition" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Course", "Deletada", "Login", "Name", "Password", "Role", "Token" },
                values: new object[,]
                {
                    { new Guid("275873ff-cc28-44d6-b845-7678af64ed50"), "Tecnologia em Desenvolvimento de aplicativos Móveis", false, "Gonçalves", "Alexandre", "3413992", 1, null },
                    { new Guid("bb55880d-9b42-4388-a688-1b7a3625467c"), "Usuario Adminitrativo", false, "adm", "Administrador", "adm", 0, null }
                });
        }
    }
}
