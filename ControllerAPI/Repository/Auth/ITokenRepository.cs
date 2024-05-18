using Microsoft.AspNetCore.Identity;

namespace ControllerAPI.Repository.Auth
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
