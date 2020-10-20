using System.Collections.Generic;
using System.Threading.Tasks;
using TestJWTCoreApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TestJWTCoreApp.Filters;
using TestJWTCoreApp.Services;

namespace TestJWTCoreApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {        
        private readonly IUsersService _usersService;
        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        [AuthorizeRoles(RolesTypes.Admin, RolesTypes.Editor)]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return await _usersService.GetUsers(); ;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<User>> Get(int id)
        {
            User user = await _usersService.GetUser(id);
            if (user == null)
                return NotFound();

            return new ObjectResult(user);
        }

        [HttpPost]
        [AuthorizeRoles(RolesTypes.Admin, RolesTypes.Editor)]
        public async Task<ActionResult<User>> Post(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            await _usersService.AddUser(user);
            return Ok(user);
        }

        [HttpPut]
        [AuthorizeRoles(RolesTypes.Admin, RolesTypes.Editor)]
        public async Task<ActionResult<User>> Put(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            if (!_usersService.IsUserExists(user.Id))
            {
                return NotFound();
            }

            await _usersService.UpdateUser(user);
            return Ok(user);
        }

        [HttpDelete("{id}")]
        [AuthorizeRoles(RolesTypes.Admin)]
        public async Task<ActionResult<User>> Delete(int id)
        {
            User user = await _usersService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }

            await _usersService.RemoveUser(user);
            return Ok(user);
        }
    }
}
