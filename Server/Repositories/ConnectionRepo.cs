using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Enums;
using Server.Models;
using Server.Repositories;

public class ConnectionRepo : IConnectionRepo
{
    private readonly StudyBuddyDbContext _context;

    public ConnectionRepo(StudyBuddyDbContext context)
    {
        _context = context;
    }

    public async Task<Connection> AddAsync(Connection connection)
    {
        _context.Connections.Add(connection);
        await _context.SaveChangesAsync();
        return connection;
    }

    public async Task<Connection?> GetByIdAsync(Guid id)
    {
        return await _context.Connections.FindAsync(id);
    }

    public async Task<IEnumerable<Connection>> GetPendingRequestsAsync(Guid receiverId)
    {
        return await _context
            .Connections.Where(c =>
                c.ReceiverId == receiverId && c.Status == ConnectionStatus.Pending
            )
            .ToListAsync();
    }

    public async Task UpdateAsync(Connection connection)
    {
        _context.Connections.Update(connection);
        await _context.SaveChangesAsync();
    }
}
