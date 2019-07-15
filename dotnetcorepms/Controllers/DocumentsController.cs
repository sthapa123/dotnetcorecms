using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using dotnetcorepms.Filters;
using dotnetcorepms.Interfaces;
using dotnetcorepms.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace dotnetcorepms.Controllers
{
    //[ValidateUserSession]
    public class DocumentsController : BaseController
    {
        private readonly IHostingEnvironment _env;
        IDocuments _IDocuments;
        ICommon _ICommon;

        public DocumentsController(IDocuments IDocuments, ICommon ICommon, IHostingEnvironment hostingEnvironment)
        {
            _IDocuments = IDocuments;
            _ICommon = ICommon;
            _env = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var repo = _IDocuments.getAllDocuments();
            return View(new DocumentsViewModelLst
            {
                dbModelLst = repo,
                ddlUser = _ICommon.GetPairModel("Users")
            });
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new DoucumentsViewModel {
                ddlUsers = _ICommon.GetPairModelWithDefault("Users"),
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DoucumentsViewModel documents)
        {
            try
            {
                var docNameExists = _IDocuments.checkDocumentnameExists(documents.dbModel.name);
                if (docNameExists)
                {
                    TempData["MessageRegistration"] = "Document Name already exists.";
                }
                else
                {
                    var uploadFileName = string.Empty;

                    if (HttpContext.Request.Form.Files != null)
                    {
                        var fileName = string.Empty;
                        string PathDB = string.Empty;

                        var files = HttpContext.Request.Form.Files;

                        if (files == null)
                        {
                            ModelState.AddModelError("", "Upload a document");
                            return View(documents.dbModel);
                        }

                        if (!ModelState.IsValid)
                        {
                            return View(documents.dbModel);
                        }

                        var uploads = Path.Combine(_env.WebRootPath, "files");
                        foreach (var file in files)
                        {
                            if (file.Length > 0)
                            {
                                fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                                if (fileName.EndsWith(".txt") || fileName.EndsWith(".pdf") || fileName.EndsWith(".docx") || fileName.EndsWith(".doc"))
                                {
                                    var myUniqueFileName = Convert.ToString(Guid.NewGuid());
                                    var FileExtension = Path.GetExtension(fileName);
                                    uploadFileName = myUniqueFileName + FileExtension;
                                    fileName = Path.Combine(_env.WebRootPath, "files") + $@"\{uploadFileName}";
                                    PathDB = uploadFileName;
                                    using (FileStream fs = System.IO.File.Create(fileName))
                                    {
                                        file.CopyTo(fs);
                                        fs.Flush();
                                    }
                                }
                                else
                                {
                                    ModelState.AddModelError("", "Invalid document format. Please upload a file with extension .pdf, .txt, .docx or .doc");
                                    return View(documents.dbModel);
                                }
                            }
                        }
                        documents.dbModel.file = PathDB;
                        documents.dbModel.created_at = documents.dbModel.updated_at = DateTime.Now;
                        if (_IDocuments.Commit(documents.dbModel, 0) > 0)
                        {
                            TempData["MessageRegistration"] = "Document Name successfully added.";
                            //return View(documents.dbModel);
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            return View(documents.dbModel);
                        }
                    }
                }
                return View(documents.dbModel);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var result = _IDocuments.getDocument(id);
            return View(new DoucumentsViewModel
            {
                dbModel = result,
                ddlUsers = _ICommon.GetPairModelWithDefault("Users"),
            });
        }

        [HttpGet]
        public IActionResult View(int id)
        {
            var result = _IDocuments.getDocument(id);
            return View(new DoucumentsViewModel
            {
                dbModel = result,
                ddlUsers = _ICommon.GetPairModelWithDefault("Users"),
            });
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var result = _IDocuments.getDocument(id);
            return View(new DoucumentsViewModel
            {
                dbModel = result,
                ddlUsers = _ICommon.GetPairModelWithDefault("Users"),
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DoucumentsViewModel entity)
        {
            try
            {

                if (_IDocuments.Commit(entity.dbModel, 1) > 0)
                {
                    TempData["MessageRegistration"] = "Document updated successfully!";
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
        public IActionResult Delete(DoucumentsViewModel entity)
        {
            try
            {

                if (_IDocuments.Commit(entity.dbModel, 2) > 0)
                {
                    TempData["MessageRegistration"] = "Document deleted successfully!";
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