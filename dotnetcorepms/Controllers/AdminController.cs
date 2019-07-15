using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dotnetcorepms.Filters;
using dotnetcorepms.Models;
using Microsoft.AspNetCore.Http;

namespace dotnetcorepms.Controllers
{
    //[ValidateAdminSession]
    public class AdminController : BaseController
    {
        public IActionResult Dashboard()
        {
            return View(new UsersViewModelLst { sessionUsername = HttpContext.Session.GetString("FullName"), });
        }
    }
}
