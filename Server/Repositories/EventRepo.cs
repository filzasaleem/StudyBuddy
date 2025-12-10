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

        public async Task<bool> DeleteEventAsync(Event ev)
        {
            _context.Events.Remove(ev);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Event?> GetEventByIdAsync(Guid id)
        {
            return await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Event>> GetEventsAsync(Guid userId)
        {
            var events = await _context.Events.Where(e => e.UserId == userId).ToListAsync();
            return events;
        }

        public async Task<Event?> UpdateEventAsync(Event updatedEvent)
        {
            var existingEvent = await _context.Events.FindAsync(updatedEvent.Id);
            if (existingEvent == null)
                return null;

            existingEvent.Title = updatedEvent.Title;
            existingEvent.Description = updatedEvent.Description;
            existingEvent.Start = updatedEvent.Start;
            existingEvent.End = updatedEvent.End;

            await _context.SaveChangesAsync();
            return existingEvent;
        }
    }
}
