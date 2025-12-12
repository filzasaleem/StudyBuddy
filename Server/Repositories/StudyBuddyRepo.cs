using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Servr.Models;

namespace Server.Repositories
{
    public class StudyBuddyRepo : IStudyBuddyRepo
    {
        private readonly StudyBuddyDbContext _context;

        public StudyBuddyRepo(StudyBuddyDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUsersWithEventsAsync(string? search)
        {
            var query = _context.Users.Include(u => u.Events).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();

                query = query
                    .Select(u => new
                    {
                        User = u,
                        ExactMatch = u.Events.Any(e =>
                            (e.Title != null && e.Title.ToLower() == search)
                            || (e.Description != null && e.Description.ToLower() == search)
                        ),
                        PartialMatch = u.Events.Any(e =>
                            (e.Title != null && e.Title.ToLower().Contains(search))
                            || (e.Description != null && e.Description.ToLower().Contains(search))
                        ),
                    })
                    .Where(x => x.ExactMatch || x.PartialMatch)
                    .OrderByDescending(x => x.ExactMatch) // exact matches first
                    .ThenByDescending(x => x.PartialMatch) // then partial matches
                    .Select(x => x.User);
            }

            return await query.ToListAsync();
        }

        public async Task<User?> GetUserWithEventsByIdAsync(Guid userId)
        {
            return await _context
                .Users.Include(u => u.Events)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }
    }
}
