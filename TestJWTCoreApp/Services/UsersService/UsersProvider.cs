using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestJWTCoreApp.Models;

namespace TestJWTCoreApp.Services
{
    public class UsersProvider : IUsersService
    {
        UsersContext db;
        public UsersProvider(UsersContext context)
        {
            db = context;
        }

        public async Task<List<User>> GetUsers()
        {
            return await db.Users.ToListAsync();
        }

        public async Task<User> GetUser(int id)
        {
            return await db.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddUser(User user)
        {
            db.Users.Add(user);
            await db.SaveChangesAsync();
        }
        public async Task UpdateUser(User user)
        {
            db.Update(user);
            await db.SaveChangesAsync();
        }
        public async Task RemoveUser(User user)
        {
            db.Users.Remove(user);
            await db.SaveChangesAsync();
        }

        public bool IsUserExists(int id)
        {
            return db.Users.Any(x => x.Id == id);
        }
    }
}
