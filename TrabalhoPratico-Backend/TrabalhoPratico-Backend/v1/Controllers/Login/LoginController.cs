using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schemas.Response;
using Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using TrabalhoPratico_Backend.Criptografia;
using TrabalhoPratico_Backend.Services.Interfaces;
using TrabalhoPratico_Backend.v1.Schemas.Request;

namespace Controllers.ControllerAuthor
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IRepository _repository;
        public LoginController(IRepository repository)
        {
            _repository = repository;
        }

        //Logar Usuário
        [HttpPost("/Login")]
        [SwaggerOperation(
         Summary = "Logar Usuário",
         Description = "Logar Usuário",
         OperationId = "login.LogarUsuario",
         Tags = new[] { "LoginEndpoints" })
        ]

        public async Task<ActionResult> HandleLogin(LoginRequestPost request)
        {
            try
            {
                //Busca login e password do usuário no banco de dados
                var user = await _repository.GetByLoginAsync<User>(request.Login);
                //verifica se não retorna nulo ou se o login esta tivo 
                if (user == null || user.Deletada == true) return BadRequest("Login não encontrado");
                //variavel que vai ser usada para encriptar a senha
                var encryptedPassword = new Cryptography();
                //Encripta a senha e verifica se é valida
                if (user.Password == encryptedPassword.Encrypt(request.Password))
                {
                    //cria um token do usuário
                    var token = TokenService.GenerateToken(user);
                    //retorna o usuario e o token
                    return Ok(LoginResponse.Response(user, token));
                }
                else
                {
                    //retorna que a senha passada está errada
                    return BadRequest("Senha não confere");
                }
            }
            catch
            {
                return BadRequest("Request inválido");
            }
        }
    }
}
