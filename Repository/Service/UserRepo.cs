using System;
using System.Collections.Generic;
using System.Text;
using Repository.Context;
using Repository.Entity;
using Repository.Interface;
using Common.Model;

namespace Repository.Service
{
    public class UserRepo : IUserRepo
    {
        private readonly FundoDBContext _userDBContext;

        public UserRepo(FundoDBContext userDBContext)
        {
            this._userDBContext = userDBContext;
        }

        public UserEntity UserRegistration(RegistrationModel registrationModel)
        {
            try
            {

                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = registrationModel.FirstName;
                userEntity.LastName = registrationModel.LastName;
                userEntity.Email = registrationModel.Email;
                userEntity.Password = registrationModel.Password;
                _userDBContext.Users.Add(userEntity);
                _userDBContext.SaveChanges();

                return userEntity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
    }
}
