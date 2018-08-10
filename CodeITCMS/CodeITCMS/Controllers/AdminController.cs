using CodeITCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeITCMS.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddMenu()
        {
            var model = new MenuModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddMenu(MenuModel model)
        {
            if (ModelState.IsValid)
            {
                var menuContext = new MenuContext
                {
                    Name = model.Name,
                    Link = model.Link,
                    TabIndex = model.TabIndex
                };

                using (var context = new ApplicationDbContext())
                {
                    context.MenuContexts.Add(menuContext);
                    context.SaveChanges();
                }

                return RedirectToAction("ViewMenu");
            }
            else
            {
                return View();
            }
        }
    }
}