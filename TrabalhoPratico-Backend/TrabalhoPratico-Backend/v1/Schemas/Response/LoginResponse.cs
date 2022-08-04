using Entities;
using System;

namespace Schemas.Response
{
    public class LoginResponse
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Token { get; set; }

        public static LoginResponse Response(User user, string token) =>
            new(user.Id, user.Name, user.Login, token);

        public LoginResponse(Guid id, string name, string login, string token)
        {
            Id = id;
            Name = name;
            Login = login;
            Token = token;
        }
    }
}