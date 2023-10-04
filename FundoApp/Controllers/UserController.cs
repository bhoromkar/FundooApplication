using Business.Interface;
using Business.Service;
using Common.Model;
using Experimental.System.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Entity;
using System;
using System.Security.Claims;

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
                return Ok(new { success = true, Message = "Login Successful", result });
            }
            return BadRequest(new { success = true, Message = "Invalid Login Details!" });
        
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
                return NotFound(new { success = false, message = "Invalid Email!" });


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPut]
        [Route("ResetPassword")]
        public IActionResult ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var result = _userBusiness.ResetPassword(email, resetPasswordModel);
                if (result == true)
                {
                    return Ok(new { success = true, message = "password reset succesfully" });
                }
                return NotFound(new { success = false, message = "Invalid Email!" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [HttpGet]
        [Route("GetUsers")]
        public IActionResult GetUsers()
        {
            try
            {
                var result = _userBusiness.GetUsers();
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}