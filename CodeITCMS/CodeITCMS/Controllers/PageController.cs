using CodeITCMS.Models;
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

        [HttpGet]
        public ActionResult GetCategoryByArticle()
        {
            var categoryByArticles = new List<CategoryByArticle>();
            using(var context = new ApplicationDbContext())
            {
                var categories = context.HelpAndAdviceCategories.ToList();
                foreach(var category in categories)
                {
                    var categoryByArticle = new CategoryByArticle();
                    var articleNumber = context.HelpAndAdviceDetails.Where(y => y.CategoryId.Equals(category.Id)).Count();
                    categoryByArticle.CategoryName = category.Category;
                    categoryByArticle.ArticleNumber = articleNumber;
                    categoryByArticle.CategoryId = category.Id;
                    categoryByArticles.Add(categoryByArticle);
                }

                return Json(categoryByArticles, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetArticleDetail(int Id)
        {
            using(var context = new ApplicationDbContext())
            {
                var article = context.HelpAndAdviceDetails.Where(y => y.Id.Equals(Id)).FirstOrDefault();
                return Json(article, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetArticleByCategory(int Id)
        {
            using(var context = new ApplicationDbContext())
            {
                var articles = context.HelpAndAdviceDetails.Where(y => y.CategoryId.Equals(Id))
                    .Select(x => new HelpAndAdviceDetailModel { Content = x.Content, Heading = x.Heading, SubHeading = x.SubHeading, Id = x.Id })
                    .FirstOrDefault();
                return Json(articles, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Blogs()
        {
            using(var context = new ApplicationDbContext())
            {
                var blogs = context.BlogContexts.ToList();
                return Json(blogs, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Blog(int Id)
        {
            using(var context = new ApplicationDbContext())
            {
                var blogDetails = context.BlogContexts.Where(y => y.Id.Equals(Id)).FirstOrDefault();
                return Json(blogDetails, JsonRequestBehavior.AllowGet);
            }
        }
    }
}