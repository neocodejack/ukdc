using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeITCMS.Controllers
{
    public class PageController : Controller
    {
        public ActionResult Index(string pageName)
        {
            ViewBag.PageName = pageName;
            return View();
        }
    }
}