using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.DTOs;
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

    public async Task<IEnumerable<NotificationDto>> GetNotificationsAsync(Guid receiverId)
    {
        return await (
            from c in _context.Connections
            join u in _context.Users on c.SenderId equals u.Id
            where c.ReceiverId == receiverId && c.Status == ConnectionStatus.Pending
            select new NotificationDto
            {
                ConnectionId = c.Id,
                SenderId = u.Id,
                SenderFirstName = u.FirstName,
                SenderLastName = u.LastName,
                SenderEmail = u.Email,
            }
        ).ToListAsync();
    }
}
