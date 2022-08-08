using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using static Entities.User;

namespace Schemas.Response
{
    //Cria o modelo de resposta do Usuário
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Course { get; set; }
        public string Role { get; set; }

        public static UserResponse Response(User user) =>
            new(user.Id, user.Name, user.Login, user.Course, user.Role);

        public UserResponse(Guid id, string name, string login, string course, AccessType role)
        {
            Id = id;
            Name = name;
            Login = login;
            Course = course;
            Role = role.ToString();
        }
    }
    //retorna lista de usuários
    public class UsersResponse
    {
        public UsersResponse(IEnumerable<User> user)
        {
            Users = user
                .Select(UserResponse.Response)
                .OrderBy(element => element.Name);
        }
        public IEnumerable<UserResponse> Users { get; set; }
    }
}