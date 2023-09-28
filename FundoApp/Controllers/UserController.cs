using Business.Interface;
using Business.Service;
using Common.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Entity;

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
        [Route("RegisterUser")]
        public IActionResult UserRegistration( RegistrationModel registrationModel)
        {
            
            if (registrationModel == null) 
            { 
                return NotFound("Deatils missing");
            }
            UserEntity user=_userBusiness.UserRegistration(registrationModel);
                return Ok(user);
        }
    }
}
