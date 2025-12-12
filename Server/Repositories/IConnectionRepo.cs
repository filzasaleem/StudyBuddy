using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Models;


namespace Server.Repositories
{
    public interface IConnectionRepo
    {
        Task<Connection> AddAsync(Connection connection);
        Task<Connection?> GetByIdAsync(Guid id);
        Task<IEnumerable<Connection>> GetPendingRequestsAsync(Guid receiverId);
        Task UpdateAsync(Connection connection);
    }
}
