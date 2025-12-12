using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Enums;
using Server.Models;

namespace Server.Services
{
    public interface IConnectionService
    {
        Task<Connection> SendRequestAsync(Guid senderId, Guid receiverId);
        Task<Connection> RespondRequestAsync(Guid connectionId, ConnectionStatus status);
        Task<IEnumerable<Connection>> GetPendingRequestsAsync(Guid receiverId);
    }
}
