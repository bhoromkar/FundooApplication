using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entity
{
    public class UserLoginEntity
    {
        public UserEntity userEntity { get; set; }

        public string Token { get; set; }
    }
}
