using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.DTOs;

namespace Server.Services
{
    public interface IEventService
    {
        Task<IEnumerable<EventDto>> GetEventsAsync(string clerkUserId);
        Task<EventDto> CreateEventAsync(string clerkUserId, EventCreateDto dto);

        Task<EventDto?> UpdateEventAsync(string clerkUserId, Guid eventId, EventCreateDto dto);
        Task<bool> DeleteEventAsync(string clerkUserId, Guid eventId);
    }
}
