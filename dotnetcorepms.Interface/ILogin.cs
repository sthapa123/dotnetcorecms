using dotnetcorepms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnetcorepms.Interfaces
{
    public interface ILogin
    {
        Users ValidateUser(string email, string passWord);
        bool UpdatePassword(Users Registration);
    }
}
