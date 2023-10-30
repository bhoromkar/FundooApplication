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
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

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

                // var result = _userDBContext.Users.FirstOrDefault(x => x.Email == userLoginToken.LoginModel.Email && x.Password == userLoginToken.LoginModel.Password);
                if (result!= null)
                {

                    var tokenString =  GenerateToken(result.Email, result.userId);
                    UserLoginEntity userLoginEntity = new UserLoginEntity();
                    userLoginEntity.Token = tokenString;
                    userLoginEntity.Email = result.Email;
                    userLoginEntity.userId=result.userId;
                    userLoginEntity.Password =Encrypt(result.Password);

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

                var result = _userDBContext.Users.FirstOrDefault(x => x.Email == user.Email && (x.Password) == user.Password);
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
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Iconfiguration["JWT:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
          new Claim(ClaimTypes.Email, email),
          new Claim("UserID", userId.ToString()),

            };
            var token = new JwtSecurityToken(Iconfiguration["JWT:Issuer"],
                Iconfiguration["JWT:Audience"], claims,
                expires: DateTime.Now.AddDays(60), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }


        public UserEntity UserRegistration(RegistrationModel registrationModel)
        {
            try
            {

                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = registrationModel.FirstName;
                userEntity.LastName = registrationModel.LastName;
                userEntity.Email = registrationModel.Email;
                userEntity.Password = Encrypt(registrationModel.Password);
                _userDBContext.Users.Add(userEntity);
                _userDBContext.SaveChanges();

                return userEntity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        /// <summary>
        /// This method is called  when the user  registration is successful and  the user  registration has been successfully.
        /// </summary>
        /// <param name="email"></param>
        /// <returns> This method return  token to user mail</returns>
        /// <exception cref="Exception"></exception>
        public string ForgetPassword(string email)
        {
            try
            {
                var Isemail = this._userDBContext.Users.Where(a => a.Email == email).FirstOrDefault();
                //user = _userDBContext.Users.FirstOrDefault(x => x.Email == user.Email);
                //long UserId = user.userId;

                
                if (Isemail != null)
                {
                    string token = GenerateToken(email, Isemail.userId);
                    string tokenwithUrl= "http://localhost:4200/reset-password/" +token;
                    MSMQService sMQService = new MSMQService();

                    sMQService.SendMessage(tokenwithUrl);

                    return token;
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
                    user.Password =Encrypt( resetPasswordModel.NewPassword);
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

        // Function to  encrypt a string
        public  string Encrypt(string password)

        {
            try
            {
                    byte[] bytes = Encoding.UTF8.GetBytes(password);
                    string encodedPassword = Convert.ToBase64String(bytes);
                    return encodedPassword;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        // Function to decrypt a string
        public  string Decrypt(string encodedPassword)
        {
            try
            {
                byte[] bytes = Convert.FromBase64String(encodedPassword);
                string decodedPassword = Encoding.UTF8.GetString(bytes);
                return decodedPassword;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

   

