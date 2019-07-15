using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using dotnetcorepms.Interfaces;
using dotnetcorepms.Models;
using dotnetcorepms.Library;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace dotnetcorepms.Controllers
{
    public class LoginController : BaseController
    {
        ILogin _ILogin;
        IUsers _IUsers;

        public LoginController(ILogin ILogin, IUsers IUsers)
        {
            _ILogin = ILogin;
            _IUsers = IUsers;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return PartialView();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            try
            {
                if (!string.IsNullOrEmpty(loginViewModel.Email) && !string.IsNullOrEmpty(loginViewModel.Password))
                {
                    var email = loginViewModel.Email;
                    var password = EncryptionLibrary.EncryptText(loginViewModel.Password);

                    var result = _ILogin.ValidateUser(email, password);

                    if (result != null)
                    {
                        if (result.ID == 0 || result.ID < 0)
                        {
                            ViewBag.errormessage = "Entered Invalid Username and Password";
                        }
                        else
                        {
                            var RoleID = result.RoleID;
                            remove_Anonymous_Cookies(); //Remove Anonymous_Cookies
                            HttpContext.Session.SetString("UserID", Convert.ToString(result.ID));
                            HttpContext.Session.SetString("RoleID", Convert.ToString(result.RoleID));
                            HttpContext.Session.SetString("Email", Convert.ToString(result.EmailID));
                            HttpContext.Session.SetString("FullName", Convert.ToString(result.Name));
                            if (RoleID == 1)
                            {
                                return RedirectToAction("Dashboard", "Admin");
                            }
                            else if (RoleID == 2)
                            {
                                return RedirectToAction("Dashboard", "Admin");
                            }
                            else if (RoleID == 3)
                            {
                                return RedirectToAction("Dashboard", "SuperAdmin");
                            }
                        }
                    }
                    else
                    {
                        ViewBag.errormessage = "Invalid Email or Password";
                        return PartialView();
                    }
                }
                return PartialView();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            try
            {
                CookieOptions option = new CookieOptions();

                if (Request.Cookies["EventChannel"] != null)
                {
                    option.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Append("EventChannel", "", option);
                }

                HttpContext.Session.Clear();
                return RedirectToAction("Login", "Login");
            }
            catch (Exception)
            {
                throw;
            }

        }

        [NonAction]
        public void remove_Anonymous_Cookies()
        {
            if (Request.Cookies["EventChannel"] != null)
            {
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Append("EventChannel", "", option);
            }
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View(new ChangePasswordModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(ChangePasswordModel ChangePasswordModel)
        {
            if (!ModelState.IsValid)
            {
                return View(ChangePasswordModel);
            }

            var password = EncryptionLibrary.EncryptText(ChangePasswordModel.Password);
            var registrationModel = _IUsers.Userinformation(Convert.ToInt32(HttpContext.Session.GetString("UserID")));

            if (registrationModel.Password == password)
            {
                var registration = new Users();
                registration.Password = EncryptionLibrary.EncryptText(ChangePasswordModel.NewPassword);
                registration.ID = Convert.ToInt32(HttpContext.Session.GetString("UserID"));
                var result = _ILogin.UpdatePassword(registration);

                if (result)
                {
                    TempData["MessageUpdate"] = "Password Updated Successfully";
                    ModelState.Clear();
                    return View(new ChangePasswordModel());
                }
                else
                {
                    return View(ChangePasswordModel);
                }
            }
            else
            {
                TempData["MessageUpdate"] = "Invalid Password";
                return View(ChangePasswordModel);
            }
        }
    }
}
