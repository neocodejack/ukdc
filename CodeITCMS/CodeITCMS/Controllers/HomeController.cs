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
                var menu = context.MenuContexts.Select(x=> new MenuModel { Name = x.Name, Link = x.Link, TabIndex = x.TabIndex, IsFooterMenuOnly = x.IsFooterMenuOnly }).ToList().OrderBy(y=>y.TabIndex);
                
                return PartialView(menu);
            }
        }

        public ActionResult GenerateFooterMenu()
        {
            using(var context = new ApplicationDbContext())
            {
                var menu = context.MenuContexts.Select(x => new MenuModel { Name = x.Name, Link = x.Link, TabIndex = x.TabIndex,IsFooterMenuOnly = x.IsFooterMenuOnly }).ToList().OrderBy(y => y.TabIndex);

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
            ViewBag.IsBlogDetailPage = false;
            if (pageName == null)
            {
                pageName = "Blog";
                ViewBag.IsBlogDetailPage = true;
            }

            using(var context = new ApplicationDbContext())
            {
                var page = context.PageContexts.Where(y => y.LinkedMenu.Equals(pageName)).FirstOrDefault();
                return PartialView("_GenerateInnerBanner", page);
            }
        }

        [HttpPost]
        public JsonResult Contact(QueryModel model)
        {
            using(var context = new ApplicationDbContext())
            {
                var queryContext = new QueryContext
                {
                    Email = model.Email,
                    Name = model.Name,
                    PhoneNumber = model.PhoneNumber,
                    Query = model.Query
                };

                context.QueryContexts.Add(queryContext);

                return Json(context.SaveChanges(), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GenerateBlog()
        {
            using(var context = new ApplicationDbContext())
            {
                var blogContext = context.BlogContexts.ToList();
                var blogs = new List<BlogModel>();
                foreach(var item in blogContext)
                {
                    var blog = new BlogModel();
                    blog.BlogContent = item.BlogContent;
                    blog.BlogDate = item.BlogDate;
                    blog.BloggerName = item.BloggerName;
                    blog.BlogName = item.BlogName;
                    blog.Id = item.Id;
                    blog.ImageName = item.ImagePath;
                    blogs.Add(blog);
                }

                return PartialView("_GenerateBlog", blogs);
            }
        }
    }
}