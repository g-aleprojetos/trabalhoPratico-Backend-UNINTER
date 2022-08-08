using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schemas.Response;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrabalhoPratico_Backend.Services.Interfaces;
using TrabalhoPratico_Backend.v1.Schemas.Request;
using TrabalhoPratico_Backend.v1.Schemas.Specification;

namespace Controllers.ControllerAuthor
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserWithBookController : ControllerBase
    {
        private IRepository _repository;
        public UserWithBookController(IRepository repository)
        {
            _repository = repository;
        }

        //Cadastra Livro usuário
        [HttpPost("/UserWithBook/Cadastro")]
        [Authorize(Roles = "ADM, USER")]
        [SwaggerOperation(
         Summary = "Cadastra Livro no Usuário",
         Description = "Cadastra Livro no Usuário",
         OperationId = "UsuarioWithBook.CadastraLivroUsuario",
         Tags = new[] { "UserWithBookEndpoints" })
        ]

        public async Task<ActionResult> HandlePostUserWithBook(UserWithBookRequest request)
        {
            try
            {
                //monta a query com specification para buscar o Usuário com os livros
                var usersSpec = new UserWithBookEspecification(request.UserId);
                //Busca todos os usuários com os livros do banco de dados
                var userList = await _repository.ListAsync(usersSpec);
                //Separa em apenas um objeto
                var user = userList.FirstOrDefault();
                //retorna não encontrado se caso os usuário vier nulo
                if (user == null || user.Deletada == true) return NotFound($"Não foi encontrado o usuario do id= {request.UserId}");
                //Busca todos os livros do banco de dados
                var bookList = await _repository.ListAsync<Book>();
                //Cria uma lista de livros
                List<Book> booksContent = new();
                //Cria uma lista de livros passado pela requisição
                var requestBooks = request.Books.ToList();

                foreach (var requestBook in requestBooks)
                {
                    //Verifica que o livro passado pela requisição tem no banco de dados
                    var bookValidation = bookList.Where(x => x.Id == requestBook.BookId && x.Deletada == false).Any();

                    if (bookValidation)//Tem o livro no banco de dados
                    {
                        //adiciona todos os livros já cadastrado do usuário na variavel booksContent
                        foreach (var bookUser in user.Books)
                        {
                            booksContent.Add(bookUser);

                        }
                        //adiciona todos os livros que não esta cadastrados no usuário na variavel booksContent
                        foreach (var book in bookList)
                        {
                            if (book.Id == requestBook.BookId)
                            {
                                booksContent.Add(book);
                                break;
                            }
                        }
                    }
                    else//Não tem o livro no banco de dados
                    {
                        return NotFound($"Não foi encontrado o Livro do id= {requestBook.BookId}");
                    }
                }
                //Atualiza o usuario com a nova lista de livros vinda da variável booksContent
                user.UpdateUserBook(booksContent);
                //Atualiza o usuario no banco de dados
                await _repository.UpdateAsync(user);
                //Retorna os dados do usuário com a lista de livros atualizada
                return Ok(ResponseUserWithBooks.ResponseAuthorWithBook(user));
            }
            catch
            {
                return BadRequest("Request inválido");
            }
        }


        //Buscar todos usuarios com livros
        [HttpGet("/UserWithBook")]
        [Authorize(Roles = "ADM")]
        [SwaggerOperation(
            Summary = "Buscar todos usuários com Livros",
            Description = "Buscar Usuario com Livros",
            OperationId = "Usuários.BuscarTodosUsuáriosLivros",
            Tags = new[] { "UserWithBookEndpoints" })
        ]
        public async Task<ActionResult> HandleGetAllUsersWithBook()
        {
            try
            {
                //monta a query com specification para buscar o Usuários com os livros
                var usersSpec = new UserWithBookEspecification();
                //Busca todos os usuários com os livros do banco de dados
                var userList = await _repository.ListAsync(usersSpec);
                //seleciona apenas os usuários ativos
                userList = userList.Where(x => x.Deletada != true).ToList();
                //retorna não encontrado se caso o userlist vier nulo
                if (userList == null) return NotFound("Não foi encontrado nenhum usuário");
                //Retorna lista de usuários com os livros
                return Ok(new UsersWithBookResponse(userList));
            }
            catch
            {
                return BadRequest("Request inválido");
            }
        }



        //Busca Usuario com Livro
        [HttpGet("/UserWithBook/{id:Guid}")]
        [Authorize(Roles = "ADM, USER")]
        [SwaggerOperation(
             Summary = "Buscar Usuario com Livros",
             Description = "Buscar Usuario com Livros",
             OperationId = "Usuario.BuscarUsuarioLivros",
             Tags = new[] { "UserWithBookEndpoints" })
        ]
        public async Task<ActionResult> HandleGetUserWithBook(Guid id)
        {
            try
            {
                //monta a query com specification para buscar o Usuário com os livros
                var usersSpec = new UserWithBookEspecification(id);
                //Busca o usuário com os livros do banco de dados
                var userList = await _repository.ListAsync(usersSpec);
                //Separa em apenas um objeto
                var user = userList.FirstOrDefault();
                //Verifica se  o usuário é nulo ou não é ativo
                if (user == null || user.Deletada == true) return NotFound($"Não foi encontrado o usuário do id= {id}");
                //retorna o usuário com o livro
                return Ok(ResponseUserWithBooks.ResponseAuthorWithBook(user));
            }
            catch
            {
                return BadRequest("Request inválido");
            }
        }


        //atualizar usuario
        [HttpPut("/UserWithBook")]
        [Authorize(Roles = "ADM, USER")]
        [SwaggerOperation(
         Summary = "Atualiza livro do Usuario",
         Description = "Atualiza livro do Usuario",
         OperationId = "UsuarioLivro.AtualizaLivroUsuario",
         Tags = new[] { "UserWithBookEndpoints" })
        ]

        public async Task<ActionResult> HandlePutUser(UserWithBookRequest request)
        {
            try
            {
                //monta a query com specification para buscar o Usuário com os livros
                var usersSpec = new UserWithBookEspecification(request.UserId);
                //Busca todos os usuários com os livros do banco de dados
                var userList = await _repository.ListAsync(usersSpec);
                //Separa em apenas um objeto
                var user = userList.FirstOrDefault();
                //Verifica se  o usuário é nulo ou não é ativo
                if (user == null || user.Deletada == true) return NotFound($"Não foi encontrado o usuario do id= {request.UserId}");
                //Busca todos os livros
                var bookList = await _repository.ListAsync<Book>();
                //Cria uma lista de livros
                List<Book> booksContent = new();
                if (request.Books != null)
                {
                    //Cria uma lista de livros passado pela requisição
                    var requestBooks = request.Books.ToList();
                    //Verifica se o livro passado pela requisição vem nulo
                    foreach (var requestBook in requestBooks)
                    {
                        //Verifica que o livro passado pela requisição tem no banco de dados
                        var bookValidation = bookList.Where(x => x.Id == requestBook.BookId && x.Deletada == false).Any();

                        if (bookValidation)//Tem o livro no banco de dados
                        {
                            foreach (var book in bookList)
                            {
                                //Atualiza toda lista de livros apagando os que tem se caso não passar junto
                                if (book.Id == requestBook.BookId)
                                {
                                    booksContent.Add(book);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            return NotFound($"Não foi encontrado o Livro do id= {requestBook.BookId}");
                        }
                    }
                }
                //Atualiza a lista de lisvro com a variavel BooksContent
                user.UpdateUserBook(booksContent);
                //Atualiza a lista de livros no bando de dados
                await _repository.UpdateAsync(user);
                //Retorna o usuário com a lista de livroa atualizada
                return Ok(ResponseUserWithBooks.ResponseAuthorWithBook(user));
            }
            catch
            {
                return BadRequest("Request inválido");
            }
        }
    }
}
