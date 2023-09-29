using Common.Model;
using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Interface
{
   public interface IUserBusiness
    {
        public UserEntity UserRegistration(RegistrationModel registrationModel);
        public UserLoginEntity UserLogin(LoginModel loginModel);
    }
}
