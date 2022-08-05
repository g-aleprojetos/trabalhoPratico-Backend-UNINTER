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
                var usersSpec = new UserWithBookEspecification(request.UserId);
                var userList = await _repository.ListAsync(usersSpec);
                var user = userList.FirstOrDefault();
                if (user == null || user.Deletada == true) return NotFound($"Não foi encontrado o usuario do id= {request.UserId}");

                var bookList = await _repository.ListAsync<Book>();
                List<Book> booksContent = new();
                var requestBooks = request.Books.ToList();

                foreach (var requestBook in requestBooks)
                {
                    var bookValidation = bookList.Where(x => x.Id == requestBook.BookId && x.Deletada == false).Any();

                    if (bookValidation)
                    {
                        foreach (var bookUser in user.Books)
                        {
                            booksContent.Add(bookUser);

                        }
                        foreach (var book in bookList)
                        {
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
                user.UpdateUserBook(booksContent);

                await _repository.UpdateAsync(user);
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
                var usersSpec = new UserWithBookEspecification();
                var userList = await _repository.ListAsync(usersSpec);
                userList = userList.Where(x => x.Deletada != true).ToList();
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
                var usersSpec = new UserWithBookEspecification(id);
                var userList = await _repository.ListAsync(usersSpec);
                var user = userList.FirstOrDefault();
                if (user == null || user.Deletada == true) return NotFound($"Não foi encontrado o usuário do id= {id}");
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
                var usersSpec = new UserWithBookEspecification(request.UserId);
                var userList = await _repository.ListAsync(usersSpec);
                var user = userList.FirstOrDefault();
                if (user == null || user.Deletada == true) return NotFound($"Não foi encontrado o usuario do id= {request.UserId}");

                var bookList = await _repository.ListAsync<Book>();
                List<Book> booksContent = new();
                var requestBooks = request.Books.ToList();

                foreach (var requestBook in requestBooks)
                {
                    var bookValidation = bookList.Where(x => x.Id == requestBook.BookId && x.Deletada == false).Any();

                    if (bookValidation)
                    {
                        foreach (var book in bookList)
                        {
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

                user.UpdateUserBook(booksContent);

                await _repository.UpdateAsync(user);
                return Ok(ResponseUserWithBooks.ResponseAuthorWithBook(user));
            }
            catch
            {
                return BadRequest("Request inválido");
            }
        }
    }
}
