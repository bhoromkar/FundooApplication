using System;
using System.Collections.Generic;
using System.Text;
using Repository.Context;
using Repository.Entity;
using Repository.Interface;
using Common.Model;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Data;

namespace Repository.Service
{
    public class UserRepo : IUserRepo
    {
        private readonly FundoDBContext _userDBContext;
        private readonly IConfiguration Iconfiguration;
       

        public UserRepo(FundoDBContext userDBContext, IConfiguration configuration)
        {
            this.Iconfiguration = configuration;
            this._userDBContext = userDBContext;
        }

        public UserLoginEntity UserLogin(LoginModel loginModel) 
        {
            try
            {
                UserEntity user = new UserEntity();
                user.Email = loginModel.Email;
                user.Password = loginModel.Password;
                var result = Authenticate(user);

               // var user = _userDBContext.Users.FirstOrDefault(x => x.Email == userLoginToken.LoginModel.Email && x.Password == userLoginToken.LoginModel.Password);
                if (user != null)
                {
                    var tokenstring = GenerateToken(user);
                    UserLoginEntity userLoginEntity = new UserLoginEntity();
                    userLoginEntity.Token = tokenstring;
                    userLoginEntity.userEntity = user;
                    return userLoginEntity;


                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public UserEntity Authenticate(UserEntity user)
        {
            try
            {
                var result = _userDBContext.Users.FirstOrDefault(x => x.Email == user.Email && x.Password == user.Password);
                if (result != null)
                {
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    

        private string GenerateToken(UserEntity user)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Iconfiguration["JWT:Key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
          new Claim(ClaimTypes.Email, user.Email.ToString()),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var Token = new JwtSecurityToken(Iconfiguration["JWT:Issuer"],
                Iconfiguration["JWT:Audience"], claims,
                expires: DateTime.Now.AddMinutes(30), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(Token);

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
        //public UserEntity Forgetpassword(forgetpass forgetpass)
        //{
          

        //}

    }
}
