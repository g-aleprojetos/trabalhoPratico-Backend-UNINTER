using Entities;
using Microsoft.AspNetCore.Mvc;
using Schemas.Response;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrabalhoPratico_Backend.Services.Interfaces;
using TrabalhoPratico_Backend.v1.Schemas.Request;

namespace Controllers.ControllerAuthor
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private IRepository _repository;
        public BookController(IRepository repository)
        {
            _repository = repository;
        }

        //Cadastra livro
        [HttpPost("/Book/Cadastro")]
        [SwaggerOperation(
         Summary = "Criar Livro",
         Description = "Criar Livro",
         OperationId = "Livro.CriarLivro",
         Tags = new[] { "BookEndpoints" })
        ]

        public async Task<ActionResult> HandlePostUser(BookRequestPost request)
        {
            try
            {
                var book = await _repository.ListAsync<Book>();
                if (book.Where(y => y.Name == request.Name && y.Deletada == false).Any()) return BadRequest("Livro já cadatrado");
                var authors = await _repository.ListAsync<Author>();
                List<Author> authorsContent = new();
                var requestAuthors = request.Authors.ToList();

                foreach (var requestAuthor in requestAuthors)
                {
                    var authorValidation = authors.Where(x => x.Name == requestAuthor.Name && x.Deletada == false).Any();

                    if (authorValidation)
                    {
                        foreach (var author in authors)
                        {
                            if (author.Name == requestAuthor.Name.ToString())
                            {
                                authorsContent.Add(author);
                                break;
                            }
                        }
                    }
                    else
                    {
                        var newAuthor = new Author(requestAuthor.Name.ToString());
                        await _repository.AddAsync(newAuthor);
                        authorsContent.Add(newAuthor);
                    }
                }

                var newBook = new Book(request.Name, request.Publisher, request.Language, request.Pages, request.Edition, request.Publication, request.Description , authorsContent);
                var createdBook = await _repository.AddAsync(newBook);
                return Ok(BookResponse.Response(createdBook));
            }
            catch
            {
                return BadRequest("Request inválido");
            }
        }

        ////Busca todos os livros
        //[HttpGet("/Book")]
        //[SwaggerOperation(
        //    Summary = "Buscar todos Livros",
        //    Description = "Buscar Livros",
        //    OperationId = "Livros.BuscarTodosLivros",
        //    Tags = new[] { "BookEndpoints" })
        //]


        //public async Task<ActionResult<IAsyncEnumerable<BookResponse>>> GetAllBook()
        //{
        //    try
        //    {
        //        var books = await _repository.ListAsync<Book>();
        //        books = books.Where(x => x.Deletada != true).ToList();
        //        return Ok(new BooksResponse(books));
        //    }
        //    catch
        //    {
        //        return BadRequest("Request inválido");
        //    }
        //}


        ////Busca um livro
        //[HttpGet("/Book/{id:Guid}")]
        //[SwaggerOperation(
        //    Summary = "Buscar um único Livro",
        //    Description = "Buscar um único Livro",
        //    OperationId = "Livro.BuscarLivro",
        //    Tags = new[] { "BookEndpoints" })
        //]

        //public async Task<ActionResult<IAsyncEnumerable<BookResponse>>> HandleGetBook(Guid id)
        //{
        //    try
        //    {
        //        var book = await _repository.GetByIdAsync<Book>(id);
        //        if (book == null || book.Deletada == true) return NotFound($"Não foi encontrado o livro do id= {id}");
        //        return Ok(BookResponse.Response(book));
        //    }
        //    catch
        //    {
        //        return BadRequest("Request inválido");
        //    }
        //}


        ////deleta um livro
        //[HttpDelete("/Book/{id:Guid}")]
        //[SwaggerOperation(
        //    Summary = "Deleta Livro",
        //    Description = "Deleta Livro",
        //    OperationId = "Livro.DeletaLivro",
        //    Tags = new[] { "BookEndpoints" })
        //]

        //public async Task<ActionResult> HandleDeleteBook(Guid id)
        //{
        //    try
        //    {
        //        var book = await _repository.GetByIdAsync<Book>(id);
        //        if (book == null || book.Deletada == true) return NotFound($"Não foi encontrado o livro do id= {id}");
        //        await _repository.DeleteLogicAsync(book);
        //        return Ok($"Livro do id={id} foi excluido com sucesso");
        //    }
        //    catch
        //    {
        //        return BadRequest("Request inválido");
        //    }
        //}
    }
}
