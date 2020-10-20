using System.ComponentModel.DataAnnotations;
using TestJWTCoreApp.Extensions;

namespace TestJWTCoreApp.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public RolesTypes Role { get; set; }        
        public string RoleDisplayName
        {
            get
            {
                return EnumExtension.GetEnumDisplayName(Role);
            }
        }
    }
}
