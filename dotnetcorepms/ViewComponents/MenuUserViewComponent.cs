using dotnetcorepms.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace dotnetcorepms.ViewComponents
{
    public class MenuUserViewComponent : ViewComponent
    {

        public MenuUserViewComponent()
        {
        }

        public IViewComponentResult Invoke(string filter)
        {
            return View();
        }
    }
}
