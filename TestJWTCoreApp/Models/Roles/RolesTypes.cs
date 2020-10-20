using System.ComponentModel.DataAnnotations;

namespace TestJWTCoreApp.Models
{    public enum RolesTypes
    {
        [Display(Name = "Администратор")]
        Admin = 0,
        [Display(Name = "Редактор")]
        Editor = 1,
        [Display(Name = "Заказчик")]
        Customer = 2
    }
}
