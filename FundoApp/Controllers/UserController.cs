using Business.Interface;
using Business.Service;
using Common.Model;
using Experimental.System.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Entity;
using System;

namespace FundoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness _userBusiness;
        public UserController(IUserBusiness userBusiness)
        {
            this._userBusiness = userBusiness;
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult UserRegistration(RegistrationModel registrationModel)
        {

            if (registrationModel == null)
            {
                return NotFound("Deatils missing");
            }
            UserEntity user = _userBusiness.UserRegistration(registrationModel);
            return Ok(user);
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult UserLogin(LoginModel loginModel)
        {
            ResponseModel responseModel = new ResponseModel();
            var result = _userBusiness.UserLogin(loginModel);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();

        }

        [HttpPost]
        [Route("ForgotPassword")]
        public IActionResult ForgetPassword(string email)
        {
            try
            {
                var result = _userBusiness.Forgetpassword(email);
                if (result != null)
                {
                    return Ok(new { success = true, message = "password link sent succesfully" });
                }
                return BadRequest(new { success = false, message = "Invalid Email!" });


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
