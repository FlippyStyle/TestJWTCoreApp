using Microsoft.AspNetCore.Authorization;
using TestJWTCoreApp.Models;

namespace TestJWTCoreApp.Filters
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params RolesTypes[] roles) 
            : base()
        {
            Roles = string.Join(",", roles);
        }
    }
}
