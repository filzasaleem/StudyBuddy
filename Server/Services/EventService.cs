using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Server.DTOs;
using Server.Repositories;
using Servr.Models;

namespace Server.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepo _eventRepo;
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;

        public EventService(IEventRepo eventRepo, IUserRepo userRepo, IMapper mapper)
        {
            _eventRepo = eventRepo;
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<EventDto> CreateEventAsync(string clerkUserId, EventCreateDto dto)
        {
            var user = await _userRepo.GetUserByClerkIdAsync(clerkUserId);
            if (user == null)
                throw new Exception("User not found");
            var newEvent = new Event
            {
                Title = dto.Title,
                Description = dto.Description,
                Start = dto.Start,
                End = dto.End,
                UserId = user.Id,
            };
            await _eventRepo.CreateEventAsync(newEvent);
            return _mapper.Map<EventDto>(newEvent);
        }

        public async Task<bool> DeleteEventAsync(string clerkUserId, Guid eventId)
        {
            var user = await _userRepo.GetUserByClerkIdAsync(clerkUserId);
            if (user == null)
                throw new Exception("User not found");

            var ev = await _eventRepo.GetEventByIdAsync(eventId);
            if (ev == null || ev.UserId != user.Id)
                return false;

            await _eventRepo.DeleteEventAsync(ev);
            return true;
        }

        public async Task<IEnumerable<EventDto>> GetEventsAsync(string clerkUserId)
        {
            var user = await _userRepo.GetUserByClerkIdAsync(clerkUserId);
            if (user == null)
                throw new Exception("User not found");

            var events = await _eventRepo.GetEventsAsync(user.Id);

            return _mapper.Map<List<EventDto>>(events);
        }

        public async Task<EventDto?> UpdateEventAsync(
            string clerkUserId,
            Guid eventId,
            EventCreateDto dto
        )
        {
            var user = await _userRepo.GetUserByClerkIdAsync(clerkUserId);
            if (user == null)
                throw new Exception("User not found");
            var ev = await _eventRepo.GetEventByIdAsync(eventId);
            if (ev == null || ev.UserId != user.Id)
                return null;
            ev.Title = dto.Title;
            ev.Description = dto.Description;
            ev.Start = dto.Start;
            ev.End = dto.End;
            await _eventRepo.UpdateEventAsync(ev);
            return _mapper.Map<EventDto>(ev);
        }
    }
}
