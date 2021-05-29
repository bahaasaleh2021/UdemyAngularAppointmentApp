using API.Entities;

namespace API.services
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}