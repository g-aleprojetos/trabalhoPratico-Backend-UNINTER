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

namespace Controllers.ControllerAuthor
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private IRepository _repository;
        public UserController(IRepository repository)
        {
            _repository = repository;
        }

        //Cadastra usuario
        [HttpPost("/User/Cadastro")]
        [Authorize(Roles = "ADM")]
        [SwaggerOperation(
         Summary = "Criar Usuário",
         Description = "Criar Usuário",
         OperationId = "Usuario.CriarUsuario",
         Tags = new[] { "UserEndpoints" })
        ]

        public async Task<ActionResult> HandlePostUser(UserRequestPost request)
        {
            try
            {
                //Busca todos os usuários do banco
                var user = await _repository.ListAsync<User>();
                //Verifica se o usuário que a requecição passou tem no banco de dados ativo 
                if (user.Where(y => y.Login == request.Login && y.Deletada == false).Any()) return BadRequest("Usuário já é cadatrado");
                //cria um usuário novo
                var newUser = new User(request.Name, request.Login, request.Password, request.Course, request.Role);
                //adiciona o usuário no banco de dados
                var createdUser = await _repository.AddAsync(newUser);
                //Retorna o usuário cadastrado
                return CreatedAtAction(nameof(HandleGetUser), new { createdUser.Id }, UserResponse.Response(createdUser));
            }
            catch
            {
                return BadRequest("Request inválido");
            }
        }

        //Buscar todos usuarios
        [HttpGet("/User")]
        [Authorize(Roles = "ADM")]
        [SwaggerOperation(
            Summary = "Buscar todos usuários",
            Description = "Buscar Usuario",
            OperationId = "Usuários.BuscarTodosUsuários",
            Tags = new[] { "UserEndpoints" })
        ]
        public async Task<ActionResult> HandleGetAllUsers()
        {
            try
            {
                //Busca lista de usuário no banco de dados
                var users = await _repository.ListAsync<User>();
                //Seleciona apenas os usuário ativos
                users = users.Where(x => x.Deletada != true).ToList();
                //retorna não encontrado se caso o user vier nulo
                if (users == null) return NotFound("Não foi encontrado nenhum usuário");
                //Retorna uma lista de usuários
                return Ok(new UsersResponse(users));
            }
            catch
            {
                return BadRequest("Request inválido");
            }
        }

        //Buscar um único usuário
        [HttpGet("/User/{id:Guid}")]
        [Authorize(Roles = "ADM,USER")]
        [SwaggerOperation(
         Summary = "Buscar um único Usuario",
         Description = "Buscar um único Usuario",
         OperationId = "Usuario.BuscarUsuario",
         Tags = new[] { "UserEndpoints" })
        ]
        public async Task<ActionResult> HandleGetUser(Guid id)
        {
            try
            {
                //Busca usuário no banco de dados
                var user = await _repository.GetByIdAsync<User>(id);
                //Verifica se o usuário não é nulo ou ativo
                if (user == null || user.Deletada == true) return NotFound($"Não foi encontrado o usuario do id= {id}");
                //rRetorna o usuário
                return Ok(UserResponse.Response(user));
            }
            catch
            {
                return BadRequest("Request inválido");
            }
        }


        //atualizar usuario
        [HttpPut("/User")]
        [Authorize(Roles = "ADM")]
        [SwaggerOperation(
         Summary = "Atualiza Usuario",
         Description = "Atualiza Usuario",
         OperationId = "Usuario.AtualizaUsuario",
         Tags = new[] { "UserEndpoints" })
        ]

        public async Task<ActionResult> HandlePutUser(UserRequestPut request)
        {
            try
            {
                //Busca usuário no banco de dados
                var user = await _repository.GetByIdAsync<User>(request.UserId);
                //Verifica se o usuário não é nulo ou ativo
                if (user == null || user.Deletada == true) return NotFound($"Não foi encontrado o usuario do id= {request.UserId}");
                //Atualiza os dados do usuário 
                user.UpdateUser(request);
                //Atualiza o uauário no banco de dados
                await _repository.UpdateAsync(user);
                //Retorna o usuário atualizado
                return Ok(UserResponse.Response(user));
            }
            catch
            {
                return BadRequest("Request inválido");
            }
        }

        //Deleta um único usuário
        [HttpDelete("/User/{id:Guid}")]
        [Authorize(Roles = "ADM")]
        [SwaggerOperation(
         Summary = "Delete Usuario",
         Description = "Delete Usuario",
         OperationId = "Usuario.DeleteUsuario",
         Tags = new[] { "UserEndpoints" })
        ]

        public async Task<ActionResult> HandleDeleteUser(Guid id)
        {
            try
            {
                ////Busca usuário no banco de dados
                var user = await _repository.GetByIdAsync<User>(id);
                //Verifica se o usuário não é nulo ou ativo
                if (user == null || user.Deletada == true) return NotFound($"Não foi encontrado o usuario do id= {id}");
                //Delata logicamente o usuário do banco de dados
                await _repository.DeleteLogicAsync(user);
                //Retorna a mensagem que o usuario foi deletado
                return Ok($"Usuario do id={id} foi excluido com sucesso");
            }
            catch
            {
                return BadRequest("Request inválido");
            }
        }
    }
}
