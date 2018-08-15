using CodeITCMS.Models;
using System;
using System.Collections.Generic;
using System.IO;
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

        [HttpGet]
        public ActionResult AddBanner()
        {
            var model = new BannerModel();
            ViewBag.Message = "";
            return View(model);
        }

        [HttpPost]
        public ActionResult AddBanner(BannerModel model)
        {
            if (ModelState.IsValid)
            {
                var bannerInfo = new BannerContext();
                bannerInfo.Title = model.Title;
                bannerInfo.SubTitle = model.SubTitle;

                if(model.File.ContentLength > 0)
                {
                    string FileName = Path.GetFileName(model.File.FileName);
                    string SavePath = Path.Combine(Server.MapPath("~/Banners"), FileName);
                    if (!Directory.Exists(Server.MapPath("~/Banners")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Banners"));
                    }
                    model.File.SaveAs(SavePath);
                    bannerInfo.ImagePath = FileName;
                }

                using(var context = new ApplicationDbContext())
                {
                    context.BannerContexts.Add(bannerInfo);
                    context.SaveChanges();
                }
            }
            ViewBag.Message = "Successfully Saved";
            return View();
        }
    }
}