using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using TestJWTCoreApp.Models;

namespace TestJWTCoreApp.Services
{
    public class TokenServiceProvider : ITokenService
    {
        UsersContext db;

        public TokenServiceProvider(UsersContext context)
        {
            db = context;
        }
        public TokenResponseModel GetToken(ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: AuthSettings.ISSUER,
                    audience: AuthSettings.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthSettings.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthSettings.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new TokenResponseModel
            {
                AccessToken = encodedJwt,
                UserName = identity.Name
            };
        }
        public ClaimsIdentity GetIdentity(LoginModel model)
        {
            User user = db.Users.FirstOrDefault(x => x.Login == model.Login && x.Password == model.Password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }
    }
}
