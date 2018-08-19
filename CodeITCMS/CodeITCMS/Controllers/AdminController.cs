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
        public ActionResult Home()
        {
            return View("Index");
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
        public ActionResult ViewPages()
        {
            var model = new List<PageModel>();
            using(var context = new ApplicationDbContext())
            {
                var contexts = context.PageContexts.ToList();

                var modelItem = new PageModel();
                foreach(var item in contexts)
                {
                    modelItem.Id = item.Id;
                    modelItem.Title = item.PageTitle;
                    modelItem.MenuName = item.LinkedMenu;
                    modelItem.Content = item.PageContent;
                    modelItem.FeatureText = item.FeatureText;

                    model.Add(modelItem);
                }
            }
            return View(model);
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

        [HttpGet]
        public ActionResult AddPage()
        {
            var pageModel = new PageModel();
            ViewBag.Menus = null;
            using(var context = new ApplicationDbContext())
            {
                ViewBag.Menus = context.MenuContexts.Select(x => new MenuDropDown { Key = x.Link, Value = x.Name }).ToList();
            }
            return View(pageModel);
        }

        [HttpPost]
        public ActionResult AddPage(PageModel model)
        {
            if (ModelState.IsValid)
            {
                var pageContent = new PageContext
                {
                    PageContent = model.Content,
                    LinkedMenu = model.MenuName,
                    PageTitle = model.Title,
                    FeatureText = model.FeatureText
                };

                if (model.File.ContentLength > 0)
                {
                    string FileName = Path.GetFileName(model.File.FileName);
                    string SavePath = Path.Combine(Server.MapPath("~/InnerBanners"), FileName);
                    if (!Directory.Exists(Server.MapPath("~/InnerBanners")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/InnerBanners"));
                    }
                    model.File.SaveAs(SavePath);
                    pageContent.FeatureImage = FileName;
                }

                using (var context = new ApplicationDbContext())
                {
                    context.PageContexts.Add(pageContent);
                    context.SaveChanges();
                }
            }

            using (var context = new ApplicationDbContext())
            {
                ViewBag.Menus = context.MenuContexts.Select(x => new MenuDropDown { Key = x.Link, Value = x.Name }).ToList();
            }

            ViewBag.Message = "Successfully Saved";
            return View();
        }

        [HttpGet]
        public ActionResult AddPhoneNumber()
        {
            var model = new PhoneModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddPhoneNumber(PhoneModel model)
        {
            if (ModelState.IsValid)
            {
                using(var context = new ApplicationDbContext())
                {
                    var existingModel = context.PhoneContexts.FirstOrDefault();
                    if(existingModel == null)
                    {
                        var phoneContext = new PhoneContext
                        {
                            Phone = model.Phone
                        };

                        context.PhoneContexts.Add(phoneContext);
                    }
                    else
                    {
                        existingModel.Phone = model.Phone;
                    }

                    context.SaveChanges();
                }
            }

            return View();
        }

        [HttpGet]
        public ActionResult UploadLogo()
        {
            var model = new LogoModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult UploadLogo(LogoModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.File.ContentLength > 0)
                {
                    string FileName = Path.GetFileName(model.File.FileName);
                    string SavePath = Path.Combine(Server.MapPath("~/Logo"), FileName);
                    if (!Directory.Exists(Server.MapPath("~/Logo")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Logo"));
                    }
                    model.File.SaveAs(SavePath);
                    var logoContext = new LogoContext
                    {
                        AltText = model.AltText,
                        LogoPath = FileName
                    };

                    using(var context = new ApplicationDbContext())
                    {
                        var logoInfo = context.LogoContexts.FirstOrDefault();
                        if(logoInfo == null)
                        {
                            context.LogoContexts.Add(logoContext);
                        }
                        else
                        {
                            logoInfo.AltText = model.AltText;
                            logoInfo.LogoPath = FileName;
                        }

                        context.SaveChanges();
                    }
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult AddFooter()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddFooter(FooterModel model)
        {
            if (ModelState.IsValid)
            {

            }
            return View();
        }

        [HttpGet]
        public ActionResult Queries()
        {
            var queryModel = new List<QueryModel>();

            using(var context = new ApplicationDbContext())
            {
                var contextData = context.QueryContexts.ToList();

                var viewModelItem = new QueryModel();
                foreach(var item in contextData)
                {
                    viewModelItem.Name = item.Name;
                    viewModelItem.PhoneNumber = item.PhoneNumber;
                    viewModelItem.Email = item.Email;
                    viewModelItem.Query = item.Query;

                    queryModel.Add(viewModelItem);
                }
            }
            return View(queryModel);
        }

    }
}