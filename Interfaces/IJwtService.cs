using Entities.Dto.User;

namespace Interfaces;

public interface IJwtService
{
    string GenerateToken(UserDto user);
}

