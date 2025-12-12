using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.DTOs;
using Server.Enums;
using Server.Models;
using Server.Repositories;

namespace Server.Services;

public class ConnectionService(IConnectionRepo repo) : IConnectionService
{
    private readonly IConnectionRepo _repo = repo;

    public async Task<Connection> SendRequestAsync(Guid senderId, Guid receiverId)
    {
        var connection = new Connection
        {
            SenderId = senderId,
            ReceiverId = receiverId,
            Status = ConnectionStatus.Pending,
        };

        await _repo.AddAsync(connection);
        return connection;
    }

    public async Task<Connection> RespondRequestAsync(
        Guid connectionId,
        ConnectionResponseDto status
    )
    {
        var connection = await _repo.GetByIdAsync(connectionId);
        if (connection == null)
            throw new Exception("Connection not found");

        var action = status.Status.ToLower();

        switch (action)
        {
            case "accept":
                connection.Status = ConnectionStatus.Accepted;
                break;

            case "reject":
            case "decline":
                connection.Status = ConnectionStatus.Rejected;
                break;

            default:
                connection.Status = ConnectionStatus.Pending;
                break;
        }

        await _repo.UpdateAsync(connection);
        return await _repo.GetByIdAsync(connectionId) ?? throw new Exception("could not updated");
    }

    public async Task<IEnumerable<Connection>> GetPendingRequestsAsync(Guid receiverId)
    {
        return await _repo.GetPendingRequestsAsync(receiverId);
    }
}
