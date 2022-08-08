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
    public class BookController : ControllerBase
    {
        private IRepository _repository;
        public BookController(IRepository repository)
        {
            _repository = repository;
        }

        //Cadastra livro
        [HttpPost("/Book/Cadastro")]
        [Authorize(Roles = "ADM")]
        [SwaggerOperation(
         Summary = "Criar Livro",
         Description = "Criar Livro",
         OperationId = "Livro.CriarLivro",
         Tags = new[] { "BookEndpoints" })
        ]

        public async Task<ActionResult> HandlePostBook(BookRequestPost request)
        {
            try
            {
                //Busca todos os livros no banco de dados
                var book = await _repository.ListAsync<Book>();
                //Verifica se o livro não existe ou não está ativo
                if (book.Where(y => y.Name == request.Name && y.Deletada == false).Any()) return BadRequest("Livro já cadatrado");
                //Busca lista de autores
                var authorsList = await _repository.ListAsync<Author>();
                //Instancia lista de autores passado pela requesição
                List<Author> authorsContent = new();
                var requestAuthors = request.Authors.ToList();

                foreach (var requestAuthor in requestAuthors)
                {
                    //verifica se o autor passado pela requesição já tem no banco
                    var authorValidation = authorsList.Where(x => x.Name == requestAuthor.Name && x.Deletada == false).Any();
                    
                    if (authorValidation)//está cadastrado no banco
                    {
                        foreach (var author in authorsList)
                        {
                            //seleciona o autor que já esta cadastrado no banco 
                            if (author.Name == requestAuthor.Name.ToString())
                            {
                                //adiciona o autor na variavel
                                authorsContent.Add(author);
                                break;
                            }
                        }
                    }
                    else//não esta cadstrado no banco
                    {
                        //Pega cria usuario com os dados da requisição
                        var newAuthor = new Author(requestAuthor.Name.ToString());
                        //adiciona o autor no banco de dados
                        await _repository.AddAsync(newAuthor);
                        //adiciona o autor na variavel
                        authorsContent.Add(newAuthor);
                    }
                }
                //Cria livro
                var newBook = new Book(request.Name, request.Publisher, request.Language, request.Pages, request.Edition, request.Publication, request.Description , authorsContent);
                //Adiciona o livro no banco de dados
                var createdBook = await _repository.AddAsync(newBook);
                //Retorna os dados do livro
                return CreatedAtAction(nameof(HandleGetBook), new { createdBook.Id }, BookResponse.Response(createdBook));
            }
            catch
            {
                return BadRequest("Request inválido");
            }
        }

        //Busca todos os livros
        [HttpGet("/Book")]
        [Authorize(Roles = "ADM,USER")]
        [SwaggerOperation(
            Summary = "Buscar todos Livros",
            Description = "Buscar Livros",
            OperationId = "Livros.BuscarTodosLivros",
            Tags = new[] { "BookEndpoints" })
        ]


        public async Task<ActionResult<IAsyncEnumerable<BookResponse>>> GetAllBook()
        {
            try
            {
                //monta a query com specification para buscar o livro com os autores
                var booksSpec = new BooksEspecification();
                //Busca todos os livros com os autores do banco de dados
                var books = await _repository.ListAsync(booksSpec);
                //seleciona apenas os livros ativos
                books = books.Where(x => x.Deletada != true).ToList();
                //retorna não encontrado se caso os livros vierem nulo
                if (books == null) return NotFound("Não foi encontrado nenhum livro");
                //retorna a lista de livros com os autores
                return Ok(new BooksResponse(books));
            }
            catch
            {
                return BadRequest("Request inválido");
            }
        }


        //Busca um livro
        [HttpGet("/Book/{id:Guid}")]
        [Authorize(Roles = "ADM,USER")]
        [SwaggerOperation(
            Summary = "Buscar um único Livro",
            Description = "Buscar um único Livro",
            OperationId = "Livro.BuscarLivro",
            Tags = new[] { "BookEndpoints" })
        ]

        public async Task<ActionResult<IAsyncEnumerable<BookResponse>>> HandleGetBook(Guid id)
        {
            try
            {
                //monta a query com specification para buscar o livro com os autores
                var bookSpec = new BooksEspecification(id);
                //Busca o livro no banco de dados
                var bookList = await _repository.ListAsync(bookSpec);
                //Separa em apenas um objeto
                var book = bookList.FirstOrDefault();
                //retorna não encontrado se caso os livros vierem nulo
                if (book == null) return NotFound($"Não foi encontrado o livro do id= {id}");
                //retorna o livro com os autores
                return Ok(BookResponse.Response(book));
            }
            catch
            {
                return BadRequest("Request inválido");
            }
        }

        //atualizar Livro
        [HttpPut("/Book")]
        [Authorize(Roles = "ADM")]
        [SwaggerOperation(
           Summary = "Atualizar Livro",
           Description = "Atualizar Livro",
           OperationId = "Livro.AtualizarLivros",
           Tags = new[] { "BookEndpoints" })
       ]

        public async Task<ActionResult> HandlePutAuthor(BookRequestPut request)
        {
            try
            {
                //monta a query com specification para buscar o livro com os autores
                var bookSpec = new BooksEspecification(request.BookId);
                //Busca o livro no banco de dados
                var bookList = await _repository.ListAsync(bookSpec);
                //Separa em apenas um objeto
                var book = bookList.FirstOrDefault();
                //retorna não encontrado se caso os livros vierem nulo
                if (book == null) return NotFound($"Não foi encontrado o livro do id= {request.BookId}");
                //Busca lista de autores          
                var authorsList = await _repository.ListAsync<Author>();
                //Instancia lista de autores passado pela requesição
                List<Author> authorsContent = new();
                //verificar se veio algum autor na requisição
                if (request.Authors != null)
                {
                    //monta uma lista com os autores vindo pela requisição
                    var requestAuthors = request.Authors.ToList();

                    foreach (var requestAuthor in requestAuthors)
                    {
                        //verifica se o autor passado pela requesição já tem no banco
                        var authorValidation = authorsList.Where(x => x.Name == requestAuthor.Name && x.Deletada == false).Any();

                        if (authorValidation)//Está cadastrado no banco
                        {
                            foreach (var author in authorsList)
                            {
                                //seleciona o autor que já esta cadastrado no banco 
                                if (author.Name == requestAuthor.Name.ToString())
                                {
                                    //adiciona o autor na variavel
                                    authorsContent.Add(author);
                                    break;
                                }
                            }
                        }
                        else//Não está cadastrado no banco
                        {
                            //Pega cria usuario com os dados da requisição
                            var newAuthor = new Author(requestAuthor.Name.ToString());
                            //adiciona o autor no banco de dados
                            await _repository.AddAsync(newAuthor);
                            //adiciona o autor na variavel
                            authorsContent.Add(newAuthor);
                        }
                    }
                }
                //Cria livro
                book.UpdateBook(request.Name, request.Publisher, request.Language, request.Pages, request.Edition, request.Publication, request.Description, authorsContent);
                //Adiciona o livro no banco de dados
                await _repository.UpdateAsync(book);
                //Retorna os dados do livro
                return Ok(BookResponse.Response(book));
            }
            catch
            {
                return BadRequest("Request inválido");
            }
        }


        //deleta um livro
        [HttpDelete("/Book/{id:Guid}")]
        [Authorize(Roles = "ADM")]
        [SwaggerOperation(
            Summary = "Deleta Livro",
            Description = "Deleta Livro",
            OperationId = "Livro.DeletaLivro",
            Tags = new[] { "BookEndpoints" })
        ]

        public async Task<ActionResult> HandleDeleteBook(Guid id)
        {
            try
            {
                //Busca o livro no banco de dados
                var book = await _repository.GetByIdAsync<Book>(id);
                //Verifica se o livro é nulo ou se está deletado
                if (book == null || book.Deletada == true) return NotFound($"Não foi encontrado o livro do id= {id}");
                //Deleta logicamente o livro no banco de dados
                await _repository.DeleteLogicAsync(book);
                //retorna o comunicado que o livro foi excluido
                return Ok($"Livro do id={id} foi excluido com sucesso");
            }
            catch
            {
                return BadRequest("Request inválido");
            }
        }
    }
}
