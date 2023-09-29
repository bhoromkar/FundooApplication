using System;
using System.Collections.Generic;
using System.Text;
using Business.Interface;
using Common.Model;
using Repository.Entity;
using Repository.Interface;
using Repository.Service;

namespace Business.Service
{
    public class UserBusiness : IUserBusiness
        
    {
        private readonly IUserRepo _userRepository;
        public UserBusiness(IUserRepo userRepository)
        {
            this._userRepository = userRepository;
        }

        public UserLoginEntity UserLogin(LoginModel loginModel)
        {
            try
            {
                return _userRepository.UserLogin(loginModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public UserEntity UserRegistration(RegistrationModel registrationModel)
        {
            try
            {
                return _userRepository.UserRegistration(registrationModel);
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
    }
}
