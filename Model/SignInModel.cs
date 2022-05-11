using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EMPLOYEE_SIGNING_AND_SIGNUP_API.Model
{
    public class SignInRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
    }
    public class SignInResponce
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
