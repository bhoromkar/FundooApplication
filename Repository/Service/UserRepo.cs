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
using Microsoft.AspNetCore.SignalR;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

                    var tokenstring = GenerateToken(user.Email, user.userId);
                    UserLoginEntity userLoginEntity = new UserLoginEntity();
                    userLoginEntity.Token = tokenstring;
                    userLoginEntity.Email = user.Email;
                    userLoginEntity.Password = user.Password;
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



        private string GenerateToken(string email, long userId)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Iconfiguration["JWT:Key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
          new Claim(ClaimTypes.Email, email),
          new Claim("UserID", userId.ToString()),

            };
            var Token = new JwtSecurityToken(Iconfiguration["JWT:Issuer"],
                Iconfiguration["JWT:Audience"], claims,
                expires: DateTime.Now.AddMinutes(60), signingCredentials: credentials);

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
        public string Forgetpassword(string email)
        {
            try
            {
                var Isemail = this._userDBContext.Users.Where(a => a.Email == email).FirstOrDefault();
                //user = _userDBContext.Users.FirstOrDefault(x => x.Email == user.Email);
                //long UserId = user.userId;


                if (Isemail != null)
                {
                    string Token = GenerateToken(email, Isemail.userId);

                    MSMQService sMQService = new MSMQService();

                    sMQService.SendMessage(Token);

                    return Token;
                }
                return null;
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

                var user = _userDBContext.Users.Where(a => a.Email == email).FirstOrDefault();
                if (user != null && resetPasswordModel.NewPassword == resetPasswordModel.ConfirmPassword)
                {
                    user.Password = resetPasswordModel.NewPassword;
                    _userDBContext.Users.Update(user);
                    _userDBContext.SaveChanges();
                    return true;
                }
                return false;
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
                return _userDBContext.Users;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

   

