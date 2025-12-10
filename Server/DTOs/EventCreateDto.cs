namespace Server.DTOs;

public record EventCreateDto(
    string Title,
    string? Description,
    DateTimeOffset Start,
    DateTimeOffset End
);
