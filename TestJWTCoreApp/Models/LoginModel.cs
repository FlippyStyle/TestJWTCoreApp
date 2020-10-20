﻿using System.ComponentModel.DataAnnotations;

namespace TestJWTCoreApp.Models
{
    public class LoginModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
