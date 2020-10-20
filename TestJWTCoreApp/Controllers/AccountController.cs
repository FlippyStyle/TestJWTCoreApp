using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using TestJWTCoreApp.Models;
using TestJWTCoreApp.Services;

namespace TestJWTCoreApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly ITokenService _tokenService;
        public AccountController(ITokenService tokenService, /*temp for initialization*/ IDbContextService dbContextService)
        {
            _tokenService = tokenService;

            // создание администратора при первом запуске (тестовые данные)
            dbContextService.CreateIfNotExists();
        }

        [HttpPost("/token")]
        public IActionResult Token(LoginModel model)
        {
            var identity = _tokenService.GetIdentity(model);
            if (identity == null)
            {
                return BadRequest(new ErrorModel("Неверный логин или пароль."));
            }

            TokenResponseModel response = _tokenService.GetToken(identity);
            return Json(response);
        }

    }
}
