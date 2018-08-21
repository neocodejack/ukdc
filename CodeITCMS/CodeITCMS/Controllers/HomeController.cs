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

        public ActionResult GenerateFooterMenu()
        {
            using(var context = new ApplicationDbContext())
            {
                var menu = context.MenuContexts.Select(x => new MenuModel { Name = x.Name, Link = x.Link, TabIndex = x.TabIndex }).ToList().OrderBy(y => y.TabIndex);

                return PartialView("_GenerateFooterMenu", menu);
            }
        }

        public ActionResult GenerateFooter()
        {
            using(var context = new ApplicationDbContext())
            {
                var content = context.FooterContexts.Select(x => new FooterModel { Content = x.Content }).FirstOrDefault();

                return PartialView("_GenerateFooter", content);
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

        public ActionResult GeneratePage(string pageName)
        {
            if (string.IsNullOrEmpty(pageName))
            {
                using(var context = new ApplicationDbContext())
                {
                    pageName = context.MenuContexts.Where(y => y.TabIndex == 0).Select(x => x.Link).FirstOrDefault();
                }
            }
                
            using(var context = new ApplicationDbContext())
            {
                var page = context.PageContexts.Where(y=>y.LinkedMenu.Equals(pageName)).Select(x => new PageModel { Content = x.PageContent, Title = x.PageTitle }).FirstOrDefault();
                return PartialView("_GeneratePage",page);
            }
        }

        public ActionResult GeneratePhone()
        {
            using(var context = new ApplicationDbContext())
            {
                var phone = context.PhoneContexts.Select(x => x.Phone).FirstOrDefault();
                return Content(phone);
            }
        }

        public ActionResult GetLogo()
        {
            using(var context = new ApplicationDbContext())
            {
                var logo = context.LogoContexts.FirstOrDefault();
                return PartialView("_GetLogo", logo);
            }
        }

        public ActionResult GenerateInnerBanner(string pageName)
        {
            using(var context = new ApplicationDbContext())
            {
                var page = context.PageContexts.Where(y => y.LinkedMenu.Equals(pageName)).FirstOrDefault();
                return PartialView("_GenerateInnerBanner", page);
            }
        }
    }
}