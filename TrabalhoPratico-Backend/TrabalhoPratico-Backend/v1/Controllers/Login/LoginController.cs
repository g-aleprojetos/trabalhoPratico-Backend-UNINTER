using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schemas.Response;
using Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
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
                var user = await _repository.GetByLoginAsync<User>(request.Login);
                if (user == null || user.Deletada == true) return BadRequest("Login não encontrado");
               
                var encryptedPassword = new Cryptography();
                if (user.Password == encryptedPassword.Encrypt(request.Password))
                {
                    var token = TokenService.GenerateToken(user);
                    return Ok(LoginResponse.Response(user, token));
                }
                else
                {
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
