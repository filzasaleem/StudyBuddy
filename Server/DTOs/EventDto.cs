namespace Server.DTOs;

public record EventDto(
    Guid Id,
    string Title,
    string? Description,
    DateTimeOffset Start,
    DateTimeOffset End
);
