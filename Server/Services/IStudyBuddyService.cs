using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.DTOs;

namespace Server.Services
{
    public interface IStudyBuddyService
    {
        Task<List<StudyBuddyCardResponse>> GetAllCardsAsync(string? search, string clerkUserId);
        Task<List<StudyBuddyCardResponse>> GetCardsByUserIdAsync(Guid userId);
    }
}
