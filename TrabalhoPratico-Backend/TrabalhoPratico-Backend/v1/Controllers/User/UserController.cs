using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schemas.Response;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Linq;
using System.Threading.Tasks;
using TrabalhoPratico_Backend.Criptografia;
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
                var user = await _repository.ListAsync<User>();
                if (user.Where(y => y.Login == request.Login && y.Deletada == false).Any()) return BadRequest("Usuário já é cadatrado");

                var encryptedPassword = new Cryptography();
                var newUser = new User(request.Name, request.Login, encryptedPassword.Encrypt(request.Password), request.Course, request.Role);
                var createdUser = await _repository.AddAsync(newUser);
                return CreatedAtAction(nameof(HandleGetUser), new { createdUser.Id }, UserResponse.Response(createdUser));
            }
            catch
            {
                return BadRequest("Request inválido");
            }
        }

        //Buscar todos usuarios
        [HttpGet("/User")]
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
                var users = await _repository.ListAsync<User>();
                users = users.Where(x => x.Deletada != true).ToList();
                return Ok(new UsersResponse(users));
            }
            catch
            {
                return BadRequest("Request inválido");
            }
        }

        //Buscar um único usuário
        [HttpGet("/User/{id:Guid}")]
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
                var user = await _repository.GetByIdAsync<User>(id);
                if (user == null || user.Deletada == true) return NotFound($"Não foi encontrado o usuario do id= {id}");
                return Ok(UserResponse.Response(user));
            }
            catch
            {
                return BadRequest("Request inválido");
            }
        }


        //atualizar usuario
        [HttpPut("/User")]
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
                var user = await _repository.GetByIdAsync<User>(request.UserId);
                if (user == null || user.Deletada == true) return NotFound($"Não foi encontrado o usuario do id= {request.UserId}");

                user.UpdateUser(request);
                await _repository.UpdateAsync(user);
                return Ok(UserResponse.Response(user));
            }
            catch
            {
                return BadRequest("Request inválido");
            }
        }

        //Deleta um único usuário
        [HttpDelete("/User/{id:Guid}")]
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
                var user = await _repository.GetByIdAsync<User>(id);
                if (user == null || user.Deletada == true) return NotFound($"Não foi encontrado o usuario do id= {id}");
                await _repository.DeleteLogicAsync(user);
                return Ok($"Usuario do id={id} foi excluido com sucesso");
            }
            catch
            {
                return BadRequest("Request inválido");
            }
        }
    }
}
