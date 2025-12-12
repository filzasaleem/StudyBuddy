using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Servr.Models;

namespace Server.Repositories
{
    public interface IStudyBuddyRepo
    {
        Task<List<User>> GetAllUsersWithEventsAsync();
        Task<User?> GetUserWithEventsByIdAsync(Guid userId);
    }
}
