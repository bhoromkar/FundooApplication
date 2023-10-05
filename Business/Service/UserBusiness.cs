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

        public string ForgetPassword(string email )
        {
            try
            {
                return _userRepository.ForgetPassword(email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                
            }
        }

        public IEnumerable<UserEntity> GetUsers()
        {
            try
            {
                return _userRepository.GetUsers();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ResetPassword(string email, ResetPasswordModel resetPasswordModel)
        {

            try
            {
                return _userRepository.ResetPassword(email, resetPasswordModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
