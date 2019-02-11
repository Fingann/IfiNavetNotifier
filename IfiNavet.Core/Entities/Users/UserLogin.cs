using System;
using IfiNavet.Core.Interfaces.Entities;

namespace IfiNavet.Core.Entities.Users
{
    public class UserLogin : EntityBase<string>
    {
        
        public UserLogin(string username, string password)
        {
            Id = Guid.NewGuid().ToString();
            Username = username;
            Password = password;           
        }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}