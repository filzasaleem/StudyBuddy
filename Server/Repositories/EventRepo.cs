using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Servr.Models;

namespace Server.Repositories
{
    public class EventRepo : IEventRepo
    {
        private readonly StudyBuddyDbContext _context;

        public EventRepo(StudyBuddyDbContext context)
        {
            _context = context;
        }

        public async Task<Event> CreateEventAsync(Event ev)
        {
            await _context.Events.AddAsync(ev);
            await _context.SaveChangesAsync();
            return ev;
        }

        public async Task<IEnumerable<Event>> GetEventsAsync(Guid userId)
        {
            var events = await _context.Events.Where(e => e.UserId == userId).ToListAsync();
            return events;
        }
    }
}
