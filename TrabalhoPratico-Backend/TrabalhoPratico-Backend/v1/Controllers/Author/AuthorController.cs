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

        public async Task<ActionResult> HandlePostAuthor(AuthorRequestPost request)
        {
            try
            {
                //Busca todos os autores do banco de dados
                var authors = await _repository.ListAsync<Author>();
                //verifica se o autor passado pela requisição já está cadastrado e ativo no banco.
                if (authors.Where(y => y.Name == request.Name && y.Deletada == false).Any()) return BadRequest("Autor já é cadatrado");
                //Cria um novo autor
                var newAuthor = new Author(request.Name);
                //cadastra o autor novo no anco de dados
                var createdAuthor = await _repository.AddAsync(newAuthor);
                //retorna o autor cadastrado
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
                //Busca todos os autores do banco de dados
                var authors = await _repository.ListAsync<Author>();
                //Seleciona somente os autores ativos
                authors = authors.Where(x => x.Deletada != true).ToList();
                //retorna não encontrado se caso os autores vierem nulo
                if (authors == null) return NotFound();
                //retorna a lista de autores
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
                //Busca o autor no banco de dados
                var author = await _repository.GetByIdAsync<Author>(id);
                //Verifica se o autor passado é nulo ou se esta ativo
                if (author == null || author.Deletada == true) return NotFound($"Não foi encontrado o autor do id= {id}");
                //retorna o autor
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
                //monta a query com specification para buscar o autor com o livro
                var authorsSpec = new AuthorsEspecification(id);
                //Busca o autor no banco de dados com os livros nele cadastrado
                var authorList = await _repository.ListAsync(authorsSpec);
                //Separa em apenas um objeto
                var author = authorList.FirstOrDefault();
                //verifica se existe um autor ou se está deletado
                if (author == null || author.Deletada == true) return NotFound($"Não foi encontrado o autor do id= {id}");
                //retorna o autor com os livros registrados
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
                //Busca o autor no banco de dados
                var author = await _repository.GetByIdAsync<Author>(request.AuthorId);
                //Verifica se o autor é nulo ou se está deletado
                if (author == null || author.Deletada == true) return NotFound($"Não foi encontrado o autor do id= {request.AuthorId}");
                //atualiza o autor
                author.UpdateAuthor(request);
                //cadastra o autor no banco de dados
                await _repository.UpdateAsync(author);
                //retorna o autor cadastrado
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
                //Busca o autor no banco de dados
                var author = await _repository.GetByIdAsync<Author>(id);
                //Verifica se o autor é nulo ou se está deletado
                if (author == null || author.Deletada == true) return NotFound($"Não foi encontrado o autor do id= {id}");
                //Deleta logicamente o autor no banco de dados
                await _repository.DeleteLogicAsync(author);
                //retorna o comunicado que o autor foi excluido
                return Ok($"Autor do id={id} foi excluido com sucesso");
            }
            catch
            {
                return BadRequest("Request inválido");
            }
        }
    }
}
