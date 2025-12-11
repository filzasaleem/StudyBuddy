using System.Text.Json.Serialization;

namespace Server.DTOs
{
    public record StudyBuddyCardResponse
    {
        public Guid UserId { get; set; }
        public string Initials { get; set; } = "";
        public string FullName { get; set; } = "";
        public bool IsOnline { get; set; } = false;
        public string Subject { get; set; } = "";
        public string? Description { get; set; } = "";
    }
}
