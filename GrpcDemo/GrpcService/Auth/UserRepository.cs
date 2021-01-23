using GrpcService.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace GrpcService.Auth
{
    internal static class UserRepository
    {
        private static readonly List<User> users = new List<User>
        {
            new User
            {
                Id = 1,
                FirstName = "Jack",
                LastName = "Brown",
                Username = "jackb",
                Password = "pass",
                Role = "user"
            },
            new User
            {
                Id = 1,
                FirstName = "John",
                LastName = "White",
                Username = "jw",
                Password = "pass",
                Role = "admin"
            }
        };

        internal static User Authenticate(string username, string password)
        {
            return users
                .Where(u => u.Username == username && u.Password == password)
                .SingleOrDefault();
        }
    }
}
