using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.DTOs;
using Server.Enums;
using Server.Models;

namespace Server.Services
{
    public interface IConnectionService
    {
        Task<Connection> SendRequestAsync(Guid senderId, Guid receiverId);
        Task<Connection> RespondRequestAsync(Guid connectionId, ConnectionResponseDto status);
        Task<IEnumerable<Connection>> GetPendingRequestsAsync(Guid receiverId);
        Task<IEnumerable<NotificationDto>> GetNotificationsAsync(Guid userId);
        Task<IEnumerable<BuddyDto>> GetBuddiesAsync(Guid userId);
        Task<IEnumerable<Connection>> GetOutgoingPendingAsync(Guid senderId);


    }
}
