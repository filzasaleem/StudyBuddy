namespace Server.DTOs;

public record UserResponse(Guid Id, string FirstName, string LastName, string Email);
