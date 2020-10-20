using System.Collections.Generic;
using System.Threading.Tasks;
using TestJWTCoreApp.Models;

namespace TestJWTCoreApp.Services
{
    public interface IDbContextService
    {
        Task<List<User>> GetUsers();
        Task<User> GetUser(int id);
        Task AddUser(User user);
        Task UpdateUser(User user);
        Task RemoveUser(User user);
        bool IsUserExists(int id);
        
        //temp for initialization
        void CreateIfNotExists();
    }
}
