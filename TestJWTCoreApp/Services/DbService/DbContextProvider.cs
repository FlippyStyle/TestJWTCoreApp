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
    }
}
