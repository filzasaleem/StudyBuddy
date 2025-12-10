namespace Server.DTOs;

public record UserResponse(string ClerkUserId, string? FirstName, string? LastName, string? Email);
