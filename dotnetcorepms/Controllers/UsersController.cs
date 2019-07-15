using dotnetcorepms.Filters;
using dotnetcorepms.Interfaces;
using dotnetcorepms.Library;
using dotnetcorepms.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace dotnetcorepms.Controllers
{
    [ValidateAdminSession]
    public class UsersController : BaseController
    {

        IUsers _IRepository;
        IRoles _IRoles;
        ICommon _ICommon;

        public UsersController(IUsers IRepository, IRoles IRoles, ICommon ICommon)
        {
            _IRepository = IRepository;
            _IRoles = IRoles;
            _ICommon = ICommon;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var repo = _IRepository.getAllUsers();
            return View(new UsersViewModelLst
            {
                dbModelLst = repo,
                ddlRoleLst = _ICommon.GetPairModel("RoleName"),
            });
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var result = _IRepository.Userinformation(id);
            return View(new UsersViewModel
            {
                dbModel = result,
                ddlRoleLst = _ICommon.GetPairModelWithDefault("RoleName"),
            });
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new UsersViewModel {
                ddlRoleLst = _ICommon.GetPairModelWithDefault("RoleName"),
            });
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var result = _IRepository.Userinformation(id);
            return View(new UsersViewModel
            {
                dbModel = result,
                ddlRoleLst = _ICommon.GetPairModelWithDefault("RoleName"),
            });
        }

        [HttpGet]
        public IActionResult View(int id)
        {
            var result = _IRepository.Userinformation(id);
            return View(new UsersViewModel
            {
                dbModel = result,
                ddlRoleLst = _ICommon.GetPairModelWithDefault("RoleName"),
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UsersViewModel entity)
        {
            try
            {
                var isUsernameExists = _IRepository.CheckUserNameExists(entity.dbModel.Username);

                if (isUsernameExists)
                {
                    ModelState.AddModelError("", errorMessage: "Username already exists. Please enter a unique username.");
                }
                else
                {
                    entity.dbModel.CreatedOn = DateTime.Now;
                    entity.dbModel.RoleID = entity.dbModel.RoleID;
                    entity.dbModel.Password = EncryptionLibrary.EncryptText(entity.dbModel.Password);
                    entity.dbModel.ConfirmPassword = EncryptionLibrary.EncryptText(entity.dbModel.Password);
                    if (_IRepository.AddUser(entity.dbModel) > 0)
                    {
                        TempData["MessageRegistration"] = "User created successfully!";
                        // return View(entity.dbModel);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }

                return RedirectToAction("Index");
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UsersViewModel entity)
        {
            try
            {

                if (_IRepository.Commit(entity.dbModel, 1) > 0)
                {
                    TempData["MessageRegistration"] = "User updated successfully!";
                    //return View(entity.dbModel);
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(UsersViewModel entity)
        {
            try
            {

                if (_IRepository.Commit(entity.dbModel, 2) > 0)
                {
                    TempData["MessageRegistration"] = "User deleted successfully!";
                    //return View(entity.dbModel);
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public JsonResult CheckUserNameExists(string Username)
        {
            try
            {
                var isUsernameExists = _IRepository.CheckUserNameExists(Username);
                if (isUsernameExists)
                {
                    return Json(data: true);
                }
                else
                {
                    return Json(data: false);
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
