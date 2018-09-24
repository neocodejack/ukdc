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

        [HttpGet]
        public ActionResult AddBlog()
        {
            var model = new BlogModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddBlog(BlogModel model)
        {
            if (ModelState.IsValid)
            {
                var Month = DateTime.Now.ToString("MMMM").Substring(0,3);
                var Year = DateTime.Now.Year;
                var Day = DateTime.Now.Day;
                var blogContext = new BlogContext
                {
                    BlogName = model.BlogName,
                    BlogContent = model.BlogContent,
                    BlogDate = Month + " " + Day + "," +Year,
                    BloggerName = model.BloggerName
                };

                using(var context = new ApplicationDbContext())
                {
                    if((model.Id == 0) || (model.Id == null))
                    {
                        context.BlogContexts.Add(blogContext);

                    }
                    else
                    {
                        var item = context.BlogContexts.Where(y => y.Id == model.Id).FirstOrDefault();
                        item.BlogContent = model.BlogContent;
                        item.BlogDate = model.BlogDate;
                        item.BloggerName = model.BloggerName;
                        item.BlogName = model.BlogName;
                    }

                    context.SaveChanges();
                }

                return RedirectToAction("ViewBlogs");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult EditBlog(int Id)
        {
            using (var context = new ApplicationDbContext())
            {
                var blogItem = context.BlogContexts.Where(y => y.Id == Id).FirstOrDefault();
                var blogModel = new BlogModel
                {
                    Id = Id,
                    BloggerName = blogItem.BloggerName,
                    BlogName = blogItem.BlogName,
                    BlogContent = blogItem.BlogContent,
                    BlogDate = blogItem.BlogDate
                };

                return View(blogModel);
            }
        }

        [HttpGet]
        public ActionResult ViewBlogs()
        {
            var blogs = new List<BlogModel>();
            using(var context = new ApplicationDbContext())
            {
                var blogsInfo = context.BlogContexts.ToList();
                foreach(var item in blogsInfo)
                {
                    var blog = new BlogModel
                    {
                        BlogContent = item.BlogContent,
                        BlogDate = item.BlogDate,
                        BloggerName = item.BloggerName,
                        BlogName = item.BlogName,
                        Id = item.Id
                    };

                    blogs.Add(blog);
                }

                return View(blogs);
            }
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
                    if ((model.Id == 0) || (model.Id == null))
                    {
                        context.MenuContexts.Add(menuContext);
                    }
                    else
                    {
                        var item = context.MenuContexts.Where(y => y.Id == model.Id).FirstOrDefault();
                        item.Link = model.Link;
                        item.Name = model.Name;
                        item.TabIndex = model.TabIndex;
                    }

                    context.SaveChanges();
                }

                return RedirectToAction("ViewMenus");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult ViewMenus()
        {
            var menuModel = new List<MenuModel>();

            using(var context = new ApplicationDbContext())
            {
                var menus = context.MenuContexts.ToList();
                
                foreach(var item in menus)
                {
                    var menuItem = new MenuModel
                    {
                        Id = item.Id,
                        Link = item.Link,
                        Name = item.Name,
                        TabIndex = item.TabIndex
                    };

                    menuModel.Add(menuItem);
                }
            }

            return View(menuModel);
        }

        [HttpGet]
        public ActionResult EditMenu(int Id)
        {
            using(var context = new ApplicationDbContext())
            {
                var menuItem = context.MenuContexts.Where(y => y.Id == Id).FirstOrDefault();
                var menuModel = new MenuModel
                {
                    Id = Id,
                    Link = menuItem.Link,
                    Name = menuItem.Name,
                    TabIndex = menuItem.TabIndex
                };

                return View(menuModel);
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
                string FileName = string.Empty;

                if (model.File.ContentLength > 0)
                {
                    FileName = Path.GetFileName(model.File.FileName);
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
                    if ((model.Id == 0) || (model.Id == null))
                    {
                        context.BannerContexts.Add(bannerInfo);
                    }
                    else
                    {
                        var item = context.BannerContexts.Where(y => y.Id == model.Id).FirstOrDefault();
                        item.ImagePath = FileName;
                        item.SubTitle = model.SubTitle;
                        item.Title = model.Title;
                    }

                    context.SaveChanges();
                }
            }

            ViewBag.Message = "Successfully Saved";
            return RedirectToAction("ViewBanners");
        }

        [HttpGet]
        public ActionResult ViewBanners()
        {
            var modelBanner = new List<BannerModel>();

            using(var context = new ApplicationDbContext())
            {
                var banners = context.BannerContexts.ToList();
                
                foreach(var item in banners)
                {
                    var modelItem = new BannerModel
                    {
                        Id = item.Id,
                        Path = item.ImagePath,
                        SubTitle = item.SubTitle,
                        Title = item.Title
                    };

                    modelBanner.Add(modelItem);
                }
            }

            return View(modelBanner);
        }

        [HttpGet]
        public ActionResult EditBanner(int Id)
        {
            using (var context = new ApplicationDbContext())
            {
                var bannerItem = context.BannerContexts.Where(y => y.Id == Id).FirstOrDefault();
                var model = new BannerModel
                {
                    Title = bannerItem.Title,
                    SubTitle = bannerItem.SubTitle,
                    Path = bannerItem.ImagePath,
                    Id = bannerItem.Id
                };

                return View(model);
            }
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
                string FileName = string.Empty;

                if ((model.File != null) && (model.File.ContentLength > 0))
                {
                    FileName = Path.GetFileName(model.File.FileName);

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
                    if ((model.Id == 0) || (model.Id==null))
                    {
                        context.PageContexts.Add(pageContent);
                    }
                    else
                    {
                        var item = context.PageContexts.Where(y => y.Id == model.Id).FirstOrDefault();
                        item.FeatureImage = FileName;
                        item.FeatureText = model.FeatureText;
                        item.LinkedMenu = model.MenuName;
                        item.PageContent = model.Content;
                        item.PageTitle = model.Title;
                    }

                    context.SaveChanges();
                }
            }

            using (var context = new ApplicationDbContext())
            {
                ViewBag.Menus = context.MenuContexts.Select(x => new MenuDropDown { Key = x.Link, Value = x.Name }).ToList();
            }

            ViewBag.Message = "Successfully Saved";
            return RedirectToAction("ViewPages");
        }

        [HttpGet]
        public ActionResult ViewPages()
        {
            var model = new List<PageModel>();
            using (var context = new ApplicationDbContext())
            {
                var contexts = context.PageContexts.ToList();

                
                foreach (var item in contexts)
                {
                    var modelItem = new PageModel
                    {
                        Id = item.Id,
                        Title = item.PageTitle,
                        MenuName = item.LinkedMenu,
                        Content = item.PageContent,
                        FeatureText = item.FeatureText,
                        FeatureImagePath = item.FeatureImage
                    };
                    model.Add(modelItem);
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult EditPage(int Id)
        {
            using(var context = new ApplicationDbContext())
            {
                var contextItem = context.PageContexts.Where(y => y.Id == Id).FirstOrDefault();
                var model = new PageModel();
                model.Id = contextItem.Id;
                model.Title = contextItem.PageTitle;
                model.Content = contextItem.PageContent;
                model.FeatureText = contextItem.FeatureText;
                model.FeatureImagePath = contextItem.FeatureImage;
                model.MenuName = contextItem.LinkedMenu;

                using (var menuContext = new ApplicationDbContext())
                {
                    ViewBag.Menus = menuContext.MenuContexts.Select(x => new MenuDropDown { Key = x.Link, Value = x.Name }).ToList();
                }

                return View(model);
            }
        }

        [HttpGet]
        public ActionResult AddPhoneNumber()
        {
            var model = new PhoneModel();
            using(var context = new ApplicationDbContext())
            {
                var phoneModel = context.PhoneContexts.FirstOrDefault();
                model.Phone = phoneModel.Phone;
                model.Id = phoneModel.Id;
            }
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
                using(var context = new ApplicationDbContext())
                {
                    if ((model.Id == null) || (model.Id==0))
                    {
                        var footerContext = new FooterContext
                        {
                            Name = model.Name,
                            Content = model.Content
                        };

                        context.FooterContexts.Add(footerContext);
                    }
                    else
                    {
                        var footerDetails = context.FooterContexts.Where(x => x.Id == model.Id).FirstOrDefault();
                        footerDetails.Content = model.Content;
                    }

                    context.SaveChanges();
                }
            }
            return RedirectToAction("AddFooter");
        }

        [HttpGet]
        public ActionResult GetFooter()
        {
            var model = new List<FooterModel>();

            using(var context = new ApplicationDbContext())
            {
                var footerItem = context.FooterContexts.ToList();

                foreach(var item in footerItem)
                {
                    var modelItem = new FooterModel
                    {
                        Content = item.Content,
                        Id = item.Id,
                        Name = item.Name
                    };

                    model.Add(modelItem);
                }
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult EditFooter(int Id)
        {
            using(var context =new ApplicationDbContext())
            {
                var footerItem = context.FooterContexts.Where(y => y.Id == Id).FirstOrDefault();
                var model = new FooterModel
                {
                    Name = footerItem.Name,
                    Content = footerItem.Content
                };

                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Queries()
        {
            var queryModel = new List<QueryModel>();

            using(var context = new ApplicationDbContext())
            {
                var contextData = context.QueryContexts.ToList();

                foreach(var item in contextData)
                {
                    var viewModelItem = new QueryModel
                    {
                        Name = item.Name,
                        PhoneNumber = item.PhoneNumber,
                        Email = item.Email,
                        Query = item.Query
                    };

                    queryModel.Add(viewModelItem);
                }
            }
            return View(queryModel);
        }

        [HttpGet]
        public ActionResult AddCategory()
        {
            var model = new HelpAndAdviceCategoryModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddCategory(HelpAndAdviceCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                using(var context = new ApplicationDbContext())
                {
                    if ((model.Id == 0) || (model.Id == null))
                    {
                        var modelContext = new HelpAndAdviceCategory
                        {
                            Category = model.Category
                        };

                        context.HelpAndAdviceCategories.Add(modelContext);
                    }
                    else
                    {
                        var modelContext = context.HelpAndAdviceCategories.Where(y => y.Id == model.Id).FirstOrDefault();
                        modelContext.Category = model.Category;
                    }
                    context.SaveChanges();
                }

                return RedirectToAction("ViewCategory");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult ViewCategory()
        {
            using(var context = new ApplicationDbContext())
            {
                var categories = new List<HelpAndAdviceCategoryModel>();
                var categoriesContext = context.HelpAndAdviceCategories.ToList();
                foreach(var item in categoriesContext)
                {
                    var category = new HelpAndAdviceCategoryModel
                    {
                        Id = item.Id,
                        Category = item.Category
                    };

                    categories.Add(category);
                }

                return View(categories);
            }
        }

        [HttpGet]
        public ActionResult EditCategory(int Id)
        {
            using(var context = new ApplicationDbContext())
            {
                var categoryInfo = context.HelpAndAdviceCategories.Where(y => y.Id == Id).FirstOrDefault();
                var categoryModel = new HelpAndAdviceCategoryModel
                {
                    Id = categoryInfo.Id,
                    Category = categoryInfo.Category
                };

                return View(categoryModel);
            }
        }

        [HttpGet]
        public ActionResult AddArticle()
        {
            var model = new HelpAndAdviceDetailModel();
            using(var context = new ApplicationDbContext())
            {
                var categories = context.HelpAndAdviceCategories.Select(x => new CategoryDropDown { Key = x.Id.ToString(), Value = x.Category }).ToList();
                ViewBag.Categories = categories;
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult EditArticle(int Id)
        {
            using(var context = new ApplicationDbContext())
            {
                var articleInfo = context.HelpAndAdviceDetails.Where(y => y.Id == Id).FirstOrDefault();
                var articleModel = new HelpAndAdviceDetailModel
                {
                    CategoryId = articleInfo.CategoryId,
                    Content = articleInfo.Content,
                    Heading = articleInfo.Heading,
                    Id = articleInfo.Id,
                    SubHeading = articleInfo.SubHeading
                };
                var categories = context.HelpAndAdviceCategories.Select(x => new CategoryDropDown { Key = x.Id.ToString(), Value = x.Category }).ToList();
                ViewBag.Categories = categories;
                return View(articleModel);
            }
        }

        [HttpGet]
        public ActionResult ViewArticle()
        {
            using(var context = new ApplicationDbContext())
            {
                var articles = new List<HelpAndAdviceDetailModel>();

                var articlesContext = context.HelpAndAdviceDetails.ToList();
                foreach(var item in articlesContext)
                {
                    var article = new HelpAndAdviceDetailModel
                    {
                        Id = item.Id,
                        Heading = item.Heading,
                        SubHeading = item.SubHeading,
                        Content = item.Content,
                        CategoryId = item.CategoryId
                    };

                    articles.Add(article);
                }

                return View(articles);
            }
        }

        [HttpPost]
        public ActionResult AddArticle(HelpAndAdviceDetailModel model)
        {
            if (ModelState.IsValid)
            {
                using (var context = new ApplicationDbContext())
                {
                    if ((model.Id == 0) || (model.Id == null))
                    {
                        var detailContext = new HelpAndAdviceDetail
                        {
                            Content = model.Content,
                            CategoryId = model.CategoryId,
                            Heading = model.Heading,
                            SubHeading = model.SubHeading,
                            LastUpdated = DateTime.Now
                        };

                        context.HelpAndAdviceDetails.Add(detailContext);
                    }
                    else
                    {
                        var detailContext = context.HelpAndAdviceDetails.Where(y => y.Id == model.Id).FirstOrDefault();
                        detailContext.Content = model.Content;
                        detailContext.CategoryId = model.CategoryId;
                        detailContext.Heading = model.Heading;
                        detailContext.SubHeading = model.SubHeading;
                        detailContext.LastUpdated = DateTime.Now;
                    }

                    context.SaveChanges();
                }

                return RedirectToAction("ViewArticle");
            }
            else
            {
                return View();
            }
        }
    }
}