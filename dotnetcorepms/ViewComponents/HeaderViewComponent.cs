﻿using Microsoft.AspNetCore.Mvc;

namespace dotnetcorepms.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {

        public HeaderViewComponent()
        {
        }

        public IViewComponentResult Invoke(string filter)
        {
            return View();
        }
    }
}
