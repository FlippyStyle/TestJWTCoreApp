using System.Security.Claims;
using TestJWTCoreApp.Models;

namespace TestJWTCoreApp.Services
{
    public interface ITokenService
    {
        TokenResponseModel GetToken(ClaimsIdentity identity);
        ClaimsIdentity GetIdentity(LoginModel model);
    }
}
