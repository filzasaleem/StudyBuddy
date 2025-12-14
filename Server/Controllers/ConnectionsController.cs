using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.DTOs;
using Server.Enums;
using Server.Models;
using Server.Services;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ConnectionsController : ControllerBase
{
    private readonly IConnectionService _service;

    public ConnectionsController(IConnectionService service)
    {
        _service = service;
    }

    // Send connection request
    [HttpPost]
    public async Task<IActionResult> SendRequest([FromBody] ConnectionRequestDto dto)
    {
        Console.WriteLine("SendRequest called");
        Console.WriteLine("SenderId: " + dto.SenderId);
        Console.WriteLine("ReceiverId: " + dto.ReceiverId);

        var connection = await _service.SendRequestAsync(dto.SenderId, dto.ReceiverId);

        // No SignalR here, the frontend will poll
        return Ok(connection);
    }

    // Respond to a connection request
    [HttpPost("{id}/respond")]
    public async Task<IActionResult> RespondRequest(Guid id, [FromBody] ConnectionResponseDto dto)
    {
        var connection = await _service.RespondRequestAsync(id, dto);

        return Ok(connection);
    }

    // Get pending requests for a user
    [HttpGet("pending/{userId}")]
    public async Task<IActionResult> GetPendingRequests(Guid userId)
    {
        var pending = await _service.GetPendingRequestsAsync(userId);
        return Ok(pending);
    }

    [HttpGet("notifications/{userId}")]
    public async Task<IActionResult> GetNotifications(Guid userId)
    {
        var notifications = await _service.GetNotificationsAsync(userId);

        var newNotification = notifications.Select(c => new
        {
            Message = $"You have a new connection request from {c.SenderFirstName ?? "Unknown"} ({c.SenderLastName ?? "Unknown"})",
            ConnectionId = c.ConnectionId,
        });

        return Ok(notifications);
    }

    [HttpGet("buddies/{userId}")]
    public async Task<IActionResult> GetBuddies(Guid userId)
    {
        var buddies = await _service.GetBuddiesAsync(userId);
        return Ok(buddies);
    }
}
