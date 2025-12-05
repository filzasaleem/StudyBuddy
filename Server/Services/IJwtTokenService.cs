using Server;

namespace Server.Services
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
    }
}
