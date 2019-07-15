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
    public class NotesController : BaseController
    {
        INotes _INotes;
        ICommon _ICommon;

        public NotesController(INotes INotes, ICommon ICommon)
        {
            _INotes = INotes;
            _ICommon = ICommon;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var repo = _INotes.getAllNotes();
            return View(new NotesViewModelLst
            {
                dbModelLst = repo,
                ddlUsers = _ICommon.GetPairModel("Users")
            });
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View(new NotesViewModel {
               ddlUsers = _ICommon.GetPairModelWithDefault("Users"),
            });
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var result = _INotes.getNote(id);
            return View(new NotesViewModel
            {
                dbModel = result,
                ddlUsers = _ICommon.GetPairModelWithDefault("Users"),
            });
        }

        [HttpGet]
        public IActionResult View(int id)
        {
            var result = _INotes.getNote(id);
            return View(new NotesViewModel
            {
                dbModel = result,
                ddlUsers = _ICommon.GetPairModelWithDefault("Users"),
            });
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var result = _INotes.getNote(id);
            return View(new NotesViewModel
            {
                dbModel = result,
                ddlUsers = _ICommon.GetPairModelWithDefault("Users"),
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NotesViewModel notes)
        {
            try
            {
                var noteNameExists = _INotes.checkNotenameExists(notes.dbModel.note);
                if (noteNameExists)
                {
                    TempData["MessageRegistration"] = "Note Name already exists.";
                }
                else
                {
                    notes.dbModel.created_at = notes.dbModel.updated_at = DateTime.Now;
                    if (_INotes.Commit(notes.dbModel, 0) > 0)
                    {
                        TempData["MessageRegistration"] = "Note Name successfully added.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View(notes.dbModel);
                    }
                }
                return View(notes.dbModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(NotesViewModel entity)
        {
            try
            {

                if (_INotes.Commit(entity.dbModel, 1) > 0)
                {
                    TempData["MessageRegistration"] = "Note updated successfully!";
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
        public IActionResult Delete(NotesViewModel entity)
        {
            try
            {

                if (_INotes.Commit(entity.dbModel, 2) > 0)
                {
                    TempData["MessageRegistration"] = "Note deleted successfully!";
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