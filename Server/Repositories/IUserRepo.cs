using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;

namespace Server.Repositories
{
    public interface IUserRepo
    {
        public Task<User> ValidateUserAsync(string email, string password);
        public Task<User> CreateUserAsync(
            string firstName,
            string lastName,
            string email,
            string password
        );
    }
}
