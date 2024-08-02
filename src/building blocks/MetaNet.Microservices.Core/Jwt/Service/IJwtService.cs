using MetaNet.Microservices.Core.Jwt.Settings;
using MetaNet.Microservices.Domain.Http.Response;

namespace MetaNet.Microservices.Core.Jwt
{
    public interface IJwtService
    {
        string GenerateToken(UserResponse user, JwtSettings jwtSettings);
    }
}
