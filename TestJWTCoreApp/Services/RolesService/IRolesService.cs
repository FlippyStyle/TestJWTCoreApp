using System.Collections.Generic;
using TestJWTCoreApp.Models;

namespace TestJWTCoreApp.Services
{
    public interface IRolesService
    {
        List<RolesViewModel> GetRolesForView();
    }
}
