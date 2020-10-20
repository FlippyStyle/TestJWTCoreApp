using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestJWTCoreApp.Models;

namespace TestJWTCoreApp.Services
{
    public class DbContextProvider : IDbContextService
    {
        UsersContext db;

        public DbContextProvider(UsersContext context)
        {
            db = context;
        }

        //temp for initialization
        public void CreateIfNotExists()
        {
            if (!db.Users.Any())
            {
                db.Users.Add(new User { Login = "Admin", Name = "Admin", Password = "12345", Email = "admin@admin.admin", Role = RolesTypes.Admin });
                db.Users.Add(new User { Login = "Editor", Name = "Editor", Password = "12345", Email = "editor@editor.editor", Role = RolesTypes.Editor });
                db.Users.Add(new User { Login = "Customer", Name = "Customer", Password = "12345", Email = "customer@customer.customer", Role = RolesTypes.Customer });
                db.SaveChanges();
            }
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
