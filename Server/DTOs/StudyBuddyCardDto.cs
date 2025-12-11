namespace Server.DTOs
{
    public record StudyBuddyCardResponse(
        Guid UserId,
        string Initials,
        string FullName,
        bool IsOnline,
        string Subject
    );
}
