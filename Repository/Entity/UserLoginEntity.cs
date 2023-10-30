using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entity
{
    public class UserLoginEntity
    {
       public string Email {  get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public long userId { get; internal set; }
    }
}
