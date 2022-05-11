using EMPLOYEE_SIGNING_AND_SIGNUP_API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMPLOYEE_SIGNING_AND_SIGNUP_API.DataAccesslayer
{
    public interface IAuthDL
    {
        public Task<SignUpResponce> SignUp(SignUpRequest signUpRequest);
        public Task<SignInResponce> SignIn(SignInRequest signInRequest);
    }
}
