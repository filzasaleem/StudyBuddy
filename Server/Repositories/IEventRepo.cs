// Server/Repositories/IEventRepository.cs
using Servr.Models;

namespace Server.Repositories;

public interface IEventRepo
{
    Task<IEnumerable<Event>> GetEventsAsync(Guid userId);
    Task<Event?> GetEventByIdAsync(Guid id);
    Task<Event> CreateEventAsync(Event ev);
    Task<bool> DeleteEventAsync(Event ev);
    Task<Event?> UpdateEventAsync(Event updatedEvent);
}
