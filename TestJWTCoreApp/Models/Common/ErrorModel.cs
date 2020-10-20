using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestJWTCoreApp.Models
{
    public class ErrorModel
    {
        public string ErrorText { get; set; }

        public ErrorModel() { }
        public ErrorModel(string error)
        {
            ErrorText = error;
        }
    }
}
