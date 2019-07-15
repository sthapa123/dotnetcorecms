using dotnetcorepms.Models;
using System;
using System.Collections.Generic;

namespace dotnetcorepms.Common
{
    /// <summary>
    /// This is where you customize the navigation sidebar
    /// </summary>
    public static class ModuleHelper
    {
        public enum Module
        {
            Dashboard,
            UserManagement,
            ChangePassword,
            Logout,
            Roles,
            Users
        }

        public static SidebarMenu AddHeader(string name)
        {
            return new SidebarMenu
            {
                Type = SidebarMenuType.Header,
                Name = name,
            };
        }

        public static SidebarMenu AddTree(string name, string iconClassName = "fa fa-link")
        {
            return new SidebarMenu
            {
                Type = SidebarMenuType.Tree,
                IsActive = false,
                Name = name,
                IconClassName = iconClassName,
                URLPath = "#",
            };
        }

        public static SidebarMenu AddModule(Module module, Tuple<int, int, int> counter = null)
        {
            if (counter == null)
                counter = Tuple.Create(0, 0, 0);

            switch (module)
            {
                case Module.Dashboard:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Dashboard",
                        IconClassName = "fa fa-dashboard",
                        URLPath = "/Admin/Dashboard",
                        LinkCounter = counter,
                    };
                case Module.UserManagement:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "User management",
                        IconClassName = "fa fa-users",
                        URLPath = "#",
                        LinkCounter = counter,
                    };
                case Module.Roles:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Roles",
                        IconClassName = "fa fa-briefcase",
                        URLPath = "/Roles/Index",
                        LinkCounter = counter,
                    };
                case Module.Users:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Users",
                        IconClassName = "fa fa-user",
                        URLPath = "/Users/Index",
                        LinkCounter = counter,
                    };
                case Module.ChangePassword:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Change Password",
                        IconClassName = "fa fa-lock",
                        URLPath = "/Login/ChangePassword",
                        LinkCounter = counter,
                    };
                case Module.Logout:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Logout",
                        IconClassName = "fa fa-sign-out",
                        URLPath = "/Login/LogOut",
                        LinkCounter = counter,
                    };       
                default:
                    break;
            }

            return null;
        }
    }
}
