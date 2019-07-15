using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetcorepms.Interfaces;
using dotnetcorepms.Models;
using dotnetcorepms.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace dotnetcorepms.Controllers
{
    public class ForumController : BaseController
    {
        IForum _IForum;

        public ForumController(IForum IForum)
        {
            _IForum = IForum;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetComments_Json()
        {
            var model = _IForum.GetComments();
            return Json(model);
        }

        [HttpPost]
        public ActionResult PostComments_Json(Forums entity)
        {
            return CRUD(entity, 0);
        }

        [HttpPost]
        public ActionResult PutComment_Json(int id, Forums entity)
        {
            return CRUD(entity, 1);
        }

        [HttpPost]
        public ActionResult DeleteComment_Json(int id, Forums entity)
        {
            return CRUD(entity, 2);
        }

        [NonAction]
        private JsonResult CRUD(Forums entity, int actionType)
        {
            int identity = 0;
            if (actionType == 2)
            {
                _IForum.Commit(entity, actionType);
                return Json(new { success = true, responseText = "Your message has been deleted!" });
            }
            else
            {
                try
                {
                    identity = _IForum.Commit(entity, actionType == 3 ? 0 : actionType);
                    var model = _IForum.GetCommentsByID(identity);
                    return Json(model);
                }
                catch (Exception)
                {
                    return Json(new { success = false, responseText = "Your message couldn't be sent!" });
                }
            }
        }
    }
}