using System.Collections.Generic;
using System.Threading.Tasks;
using TestJWTCoreApp.Models;

namespace TestJWTCoreApp.Services
{
    public interface IDbContextService
    {        
        //temp for initialization
        void CreateIfNotExists();
    }
}
