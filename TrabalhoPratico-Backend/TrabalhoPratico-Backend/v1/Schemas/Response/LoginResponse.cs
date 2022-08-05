using Entities;
using System;
using static Entities.User;

namespace Schemas.Response
{
    public class LoginResponse
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }

        public static LoginResponse Response(User user, string token) =>
            new(user.Id, user.Name, user.Login, user.Role ,token);

        public LoginResponse(Guid id, string name, string login, AccessType role ,string token)
        {
            Id = id;
            Name = name;
            Login = login;
            Role = role.ToString();
            Token = token;
        }
    }
}