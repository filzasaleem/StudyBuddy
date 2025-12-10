// Server/Repositories/IEventRepository.cs
using Servr.Models;

namespace Server.Repositories;

public interface IEventRepo
{
    Task<IEnumerable<Event>> GetEventsAsync(Guid userId);
    Task<Event> CreateEventAsync(Event ev);

}
