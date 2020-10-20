using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestJWTCoreApp.Models;
using TestJWTCoreApp.Services;

namespace TestJWTCoreApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RolesController : Controller
    {
        private readonly IRolesService _rolesService;
        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }
        [HttpGet]
        public ActionResult<List<RolesViewModel>> Get()
        {
            List<RolesViewModel> result = _rolesService.GetRolesForView();
            return Ok(result);
        }
    }
}
