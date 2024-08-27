using Microsoft.AspNetCore.Identity;

namespace Libook_API.Service.AuthService
{
    public interface ITokenService
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
