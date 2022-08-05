using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schemas.Response;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Linq;
using System.Threading.Tasks;
using TrabalhoPratico_Backend.Services.Interfaces;
using TrabalhoPratico_Backend.v1.Schemas.Request;
using TrabalhoPratico_Backend.v1.Schemas.Specification;

namespace Controllers.ControllerAuthor
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private IRepository _repository;
        public AuthorController(IRepository repository)
        {
            _repository = repository;
        }

        //cadastra um autor
        [HttpPost("/Author/Cadastro")]
        [Authorize(Roles = "ADM")]
        [SwaggerOperation(
            Summary = "Cadastrar Autor",
            Description = "Cadastrar Autor",
            OperationId = "Atores.CadastrarAutor",
            Tags = new[] { "AuthorEndpoints" })
        ]

        public async Task<ActionResult> HandlePostUser(AuthorRequestPost request)
        {
            try
            {
                var authors = await _repository.ListAsync<Author>();
                if (authors.Where(y => y.Name == request.Name && y.Deletada == false).Any()) return BadRequest("Autor já é cadatrado");

                var newAuthor = new Author(request.Name);
                var createdAuthor = await _repository.AddAsync(newAuthor);
                return CreatedAtAction(nameof(HandleGetAuthor), new { createdAuthor.Id }, AuthorResponse.Response(createdAuthor));
            }
            catch
            {
                return BadRequest("Request inválido");
            }
        }


        //Busca todos os Autores
        [HttpGet("/Author")]
        [Authorize(Roles = "ADM,USER")]
        [SwaggerOperation(
            Summary = "Buscar todos Autores",
            Description = "Buscar Autores",
            OperationId = "Autores.BuscarTodosAutores",
            Tags = new[] { "AuthorEndpoints" })
        ]
        public async Task<ActionResult> GetAllAuthor()
        {
            try
            {
                var authors = await _repository.ListAsync<Author>();
                authors = authors.Where(x => x.Deletada != true).ToList();
                return Ok(new AuthorsResponse(authors));
            }
            catch
            {
                return BadRequest("Request inválido");
            }
        }


        //Busca somente um Autor
        [HttpGet("/Author/{id:Guid}")]
        [Authorize(Roles = "ADM,USER")]
        [SwaggerOperation(
            Summary = "Buscar um único Autor",
            Description = "Buscar um único Autor",
            OperationId = "Autor.BuscarAutor",
            Tags = new[] { "AuthorEndpoints" })
        ]
        public async Task<ActionResult> HandleGetAuthor(Guid id)
        {
            try
            {
                var author = await _repository.GetByIdAsync<Author>(id);
                if (author == null || author.Deletada == true) return NotFound($"Não foi encontrado o autor do id= {id}");
                return Ok(AuthorResponse.Response(author));
            }
            catch
            {
                return BadRequest("Request inválido");
            }
        }

        //Busca Autor com Livro
        [HttpGet("/AuthorWithBook/{id:Guid}")]
        [Authorize(Roles = "ADM,USER")]
        [SwaggerOperation(
            Summary = "Buscar Autor com Livros",
            Description = "Buscar Autor com livros",
            OperationId = "AutorWithBook.BuscarAutorComLivro",
            Tags = new[] { "AuthorEndpoints" })
        ]
        public async Task<ActionResult> HandleGetAuthorWithBook(Guid id)
        {
            try
            {
                var authorsSpec = new AuthorsEspecification(id);
                var authorList = await _repository.ListAsync(authorsSpec);
                var author = authorList.FirstOrDefault();
                if (author == null || author.Deletada == true) return NotFound($"Não foi encontrado o autor do id= {id}");
                return Ok(ResponseAuthorWithBooks.ResponseAuthorWithBook(author));
            }
            catch
            {
                return BadRequest("Request inválido");
            }
        }

        //atualizar usuario
        [HttpPut("/Author")]
        [Authorize(Roles = "ADM")]
        [SwaggerOperation(
            Summary = "Atualiza Autor",
            Description = "Atualiza Autor",
            OperationId = "Autor.AtualizaAutor",
            Tags = new[] { "AuthorEndpoints" })
        ]

        public async Task<ActionResult> HandlePutAuthor(AuthorRequest request)
        {
            try
            {
                var author = await _repository.GetByIdAsync<Author>(request.AuthorId);
                if (author == null || author.Deletada == true) return NotFound($"Não foi encontrado o autor do id= {request.AuthorId}");

                author.UpdateAuthor(request);
                await _repository.UpdateAsync(author);
                return Ok(AuthorResponse.Response(author));
            }
            catch
            {
                return BadRequest("Request inválido");
            }
        }

        //deleta um autor
        [HttpDelete("/Author/{id:Guid}")]
        [Authorize(Roles = "ADM")]
        [SwaggerOperation(
            Summary = "Deleta Autor",
            Description = "Deleta Autor",
            OperationId = "Autor.deleteAutor",
            Tags = new[] { "AuthorEndpoints" })
        ]
        public async Task<ActionResult> HandleDeleteAuthor(Guid id)
        {
            try
            {
                var author = await _repository.GetByIdAsync<Author>(id);
                if (author == null || author.Deletada == true) return NotFound($"Não foi encontrado o autor do id= {id}");
                await _repository.DeleteLogicAsync(author);
                return Ok($"Autor do id={id} foi excluido com sucesso");
            }
            catch
            {
                return BadRequest("Request inválido");
            }
        }
    }
}
