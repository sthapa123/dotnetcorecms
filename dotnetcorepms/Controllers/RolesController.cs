using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetcorepms.Filters;
using dotnetcorepms.Interfaces;
using dotnetcorepms.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnetcorepms.Controllers
{
    //[ValidateUserSession]
    public class RolesController : BaseController
    {
        IRoles _IRoles;
        ICommon _ICommon;

        public RolesController(IRoles IRoles, ICommon ICommon)
        {
            _IRoles = IRoles;
            _ICommon = ICommon;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var repo = _IRoles.getAllRoles();
            return View(new RolesViewModelLst
            {
                dbModelLst = repo
            });
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var result = _IRoles.getRole(id);
            return View(new RolesViewModel
            {
                dbModel = result,
            });
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var result = _IRoles.getRole(id);
            return View(new RolesViewModel
            {
                dbModel = result,
            });
        }

        [HttpGet]
        public IActionResult View(int id)
        {
            var result = _IRoles.getRole(id);
            return View(new RolesViewModel
            {
                dbModel = result,
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Roles roles)
        {
            try
            {
                var roleNameExists = _IRoles.checkRolenameExists(roles.Rolename);
                if (roleNameExists)
                {
                    TempData["MessageRegistration"] = "Role Name already exists.";
                }
                else
                {
                    roles.Created_At = DateTime.Now;
                    roles.Updated_At = DateTime.Now;
                    if (_IRoles.addRole(roles) > 0)
                    {
                        TempData["MessageRegistration"] = "Role Name successfully added.";
                        return View(roles);
                    }
                    else
                    {
                        return View(roles);
                    }
                }
                return View(roles);
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(RolesViewModel entity)
        {
            try
            {

                if (_IRoles.Commit(entity.dbModel, 1) > 0)
                {
                    TempData["MessageRegistration"] = "Role updated successfully!";
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
        public IActionResult Delete(RolesViewModel entity)
        {
            try
            {

                if (_IRoles.Commit(entity.dbModel, 2) > 0)
                {
                    TempData["MessageRegistration"] = "Role deleted successfully!";
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
    }
}