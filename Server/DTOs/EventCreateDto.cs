namespace Server.DTOs;

public class EventCreateDto(
    string Title,
    string? Description,
    DateTimeOffset Start,
    DateTimeOffset End
);
