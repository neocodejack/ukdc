using CodeITCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeITCMS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult GenerateMenu()
        {
            using(var context = new ApplicationDbContext())
            {
                var menu = context.MenuContexts.Select(x=> new MenuModel { Name = x.Name, Link = x.Link, TabIndex = x.TabIndex }).ToList().OrderBy(y=>y.TabIndex);
                
                return PartialView(menu);
            }
        }

        public ActionResult GenerateBanner()
        {
            using(var context = new ApplicationDbContext())
            {
                var banner = context.BannerContexts.Select(x => new BannerModel { Title = x.Title, SubTitle = x.SubTitle, Path = x.ImagePath }).ToList();

                return PartialView(banner);
            }
        }
    }
}