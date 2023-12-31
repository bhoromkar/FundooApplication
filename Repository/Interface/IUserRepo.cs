﻿using Common.Model;
using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IUserRepo
    {
        public UserEntity UserRegistration(RegistrationModel registrationModel);
        public  UserLoginEntity UserLogin(LoginModel loginModel);
        public string ForgetPassword(string email);
        public bool ResetPassword(string email, ResetPasswordModel resetPasswordModel);
        public IEnumerable<UserEntity> GetUsers();
       
    }
}
