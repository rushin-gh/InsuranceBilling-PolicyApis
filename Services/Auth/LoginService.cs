using Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Auth
{
    public class LoginService
    {
        public bool IsValidUser(LoginModel loginModel)
        {
            return (loginModel.username == "rushi" && loginModel.password == "rushidev");
        }
    }
}
