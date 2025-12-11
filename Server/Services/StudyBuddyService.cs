using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Server.DTOs;
using Server.Repositories;

namespace Server.Services
{
    public class StudyBuddyService : IStudyBuddyService
    {
        private readonly IStudyBuddyRepo _repo;
        private readonly IMapper _mapper;

        public StudyBuddyService(IStudyBuddyRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<StudyBuddyCardResponse>> GetAllCardsAsync()
        {
            var users = await _repo.GetAllUsersWithEventsAsync();
            if (users == null)
                return new List<StudyBuddyCardResponse>();
            var cards = users
                .Where(u => u.Events != null && u.Events.Any()) // skip users with no subjects
                .SelectMany(u =>
                    u.Events.Select(e =>
                    {
                        var card = _mapper.Map<StudyBuddyCardResponse>(u);
                        card.Subject = e.Title;
                        card.IsOnline = false;
                        return card;
                    })
                )
                .ToList();

            return cards;
        }

        public async Task<List<StudyBuddyCardResponse>> GetCardsByUserIdAsync(Guid userId)
        {
            var user = await _repo.GetUserWithEventsByIdAsync(userId);
            if (user == null || user.Events == null || !user.Events.Any())
                return new List<StudyBuddyCardResponse>();
            var cards = user
                .Events.Select(e =>
                {
                    var card = _mapper.Map<StudyBuddyCardResponse>(user);
                    card.Subject = e.Title;
                    card.IsOnline = false;
                    return card;
                })
                .ToList();

            return cards;
        }
    }
}
