using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using dotnetcorepms.Common;
using dotnetcorepms.Models;
using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Http;

namespace dotnetcorepms.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {
        public SidebarViewComponent()
        {
        }

        public IViewComponentResult Invoke(string filter)
        {
            //you can do the access rights checking here by using session, user, and/or filter parameter
            var sidebars = new List<SidebarMenu>();
            string UserCurrentRole = (string)HttpContext.Session.GetString("RoleID");

            //sidebars.Add(ModuleHelper.AddHeader("MAIN NAVIGATION"));
            sidebars.Add(ModuleHelper.AddModule(ModuleHelper.Module.Dashboard));
            if (UserCurrentRole == "1")
            {
                sidebars.Add(ModuleHelper.AddTree("User Management", "fa fa-users"));
                sidebars.Last().TreeChild = new List<SidebarMenu>()
            {
                ModuleHelper.AddModule(ModuleHelper.Module.Roles),
                ModuleHelper.AddModule(ModuleHelper.Module.Users, Tuple.Create(1, 1, 1)),
            };
            }
            sidebars.Add(ModuleHelper.AddModule(ModuleHelper.Module.ChangePassword, Tuple.Create(0, 1, 0)));
            sidebars.Add(ModuleHelper.AddModule(ModuleHelper.Module.Logout, Tuple.Create(1, 0, 0)));


            return View(sidebars);
        }
    }
}
