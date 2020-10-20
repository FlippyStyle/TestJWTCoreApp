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
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IDbContextService _dbContextService;
        public UsersController(IDbContextService dbContextService)
        {
            _dbContextService = dbContextService;
        }

        [HttpGet]
        [AuthorizeRoles(RolesTypes.Admin, RolesTypes.Editor)]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return await _dbContextService.GetUsers(); ;
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            User user = await _dbContextService.GetUser(id);
            if (user == null)
                return NotFound();

            return new ObjectResult(user);
        }

        // POST api/users
        [HttpPost]
        [Authorize]
        [AuthorizeRoles(RolesTypes.Admin, RolesTypes.Editor)]
        public async Task<ActionResult<User>> Post(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            await _dbContextService.AddUser(user);
            return Ok(user);
        }

        // PUT api/users/
        [HttpPut]
        [Authorize]
        [AuthorizeRoles(RolesTypes.Admin, RolesTypes.Editor)]
        public async Task<ActionResult<User>> Put(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            if (!_dbContextService.IsUserExists(user.Id))
            {
                return NotFound();
            }

            await _dbContextService.UpdateUser(user);
            return Ok(user);
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        [Authorize]
        [AuthorizeRoles(RolesTypes.Admin)]
        public async Task<ActionResult<User>> Delete(int id)
        {
            User user = await _dbContextService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }

            await _dbContextService.RemoveUser(user);
            return Ok(user);
        }
    }
}
