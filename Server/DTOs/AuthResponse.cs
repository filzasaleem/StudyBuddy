using Server.DTOs;

public class AuthResponse
{
    public UserResponse User { get; set; }
    public string Token { get; set; }
}
