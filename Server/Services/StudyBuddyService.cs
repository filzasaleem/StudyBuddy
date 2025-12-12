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

        public async Task<List<StudyBuddyCardResponse>> GetAllCardsAsync(string? search)
        {
            var users = await _repo.GetAllUsersWithEventsAsync();
            if (users == null)
                return new List<StudyBuddyCardResponse>();

            search = search?.ToLower().Trim();

            var cards = users
                .Where(u => u.Events != null && u.Events.Any())
                .SelectMany(u =>
                    u.Events.Select(e =>
                    {
                        var card = _mapper.Map<StudyBuddyCardResponse>(u);
                        card.Subject = e.Title;
                        card.Description = e.Description;

                        // Calculate score
                        int score = 0;
                        if (!string.IsNullOrWhiteSpace(search))
                        {
                            if (!string.IsNullOrEmpty(e.Title))
                            {
                                var titleLower = e.Title.ToLower() ?? "";
                                if (titleLower == search)
                                    score += 100;
                                else if (titleLower.StartsWith(search))
                                    score += 50;
                                else if (titleLower.Contains(search))
                                    score += 10;
                            }

                            if (!string.IsNullOrEmpty(e.Description))
                            {
                                var descLower = e.Description.ToLower() ?? "";
                                if (descLower == search)
                                    score += 100;
                                else if (descLower.StartsWith(search))
                                    score += 50;
                                else if (descLower.Contains(search))
                                    score += 10;
                            }
                        }

                        return new { Card = card, Score = score };
                    })
                )
                .Where(x => x.Score > 0 || string.IsNullOrWhiteSpace(search)) // filter only matches, or include all if no search
                .OrderByDescending(x => x.Score) // exact -> startsWith -> contains
                .Select(x => x.Card)
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
                    card.Description = e.Description;
                    return card;
                })
                .ToList();

            return cards;
        }
    }
}
