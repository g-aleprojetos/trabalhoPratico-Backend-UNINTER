using static Entities.User;

namespace TrabalhoPratico_Backend.v1.Schemas.Request
{
    //Cria o modelo de requisição do post
    public class UserRequestPost
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Course { get; set; }
        public AccessType Role { get; set; }
    }
}
