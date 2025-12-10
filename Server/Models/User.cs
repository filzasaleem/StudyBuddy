namespace Servr.Models;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ClerkUserId { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<Event> Events { get; set; } = new();
}
