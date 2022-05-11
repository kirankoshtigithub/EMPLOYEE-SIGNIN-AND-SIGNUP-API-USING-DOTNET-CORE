using EMPLOYEE_SIGNING_AND_SIGNUP_API.DataAccesslayer;
using EMPLOYEE_SIGNING_AND_SIGNUP_API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMPLOYEE_SIGNING_AND_SIGNUP_API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly IAuthDL _authDL;
        public AuthController(IAuthDL authDL)
        {
            _authDL = authDL;
        }
        [HttpPost]
        public async Task<ActionResult> SignUp(SignUpRequest request)
        {
            SignUpResponce responce = new SignUpResponce();
            try
            {
                responce = await _authDL.SignUp(request);
            }
            catch (Exception ex)
            {
                responce.IsSuccess = false;
                responce.Message = ex.Message;
            }
            return Ok(responce);
        }
        [HttpPost]
        public async Task<ActionResult> SignIn(SignInRequest request)
        {
            SignInResponce responce = new SignInResponce();
            try
            {
                responce = await _authDL.SignIn(request);
            }
            catch (Exception ex)
            {
                responce.IsSuccess = false;
                responce.Message = ex.Message;
            }
            return Ok(responce);
        }
    }
}
