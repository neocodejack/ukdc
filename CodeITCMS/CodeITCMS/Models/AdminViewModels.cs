using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeITCMS.Models
{
    public class MenuModel
    {
        public int? Id { get; set; }

        [Required]
        [Display(Name="Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name="Link to page")]
        public string Link { get; set; }

        [Required]
        [Display(Name="Menu Index")]
        public int TabIndex { get; set; }
    }

    public class BannerModel
    {
        public int? Id { get; set; }

        [Required]
        [Display(Name ="Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name ="Sub Title")]
        public string SubTitle { get; set; }

        public string Path { get; set; }

        public HttpPostedFileBase File { get; set; }
    }

    public class PageModel
    {
        public int? Id { get; set; }

        [Required]
        [Display(Name ="Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name ="Content")]
        [UIHint("tinymce_jquery_full"), AllowHtml]
        public string Content { get; set; }

        [Required]
        [Display(Name ="Menu")]
        public string MenuName { get; set; }

        //[Required]
        [Display(Name ="Feature Image")]
        public HttpPostedFileBase File { get; set; }

        [Required]
        [Display(Name ="Feature Text")]
        public string FeatureText { get; set; }

        public string FeatureImagePath { get; set; }
    }

    public class MenuDropDown
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class CategoryDropDown
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class PhoneModel
    {
        public int? Id { get; set; }
        
        [Required]
        [Display(Name ="Phone")]
        public string Phone { get; set; }
    }

    public class LogoModel
    {
        public int Id { get; set; }
        
        [Required]
        [Display(Name ="Image Alt Text")]
        public string AltText { get; set; }

        public HttpPostedFileBase File { get; set; }
    }

    public class FooterModel
    {
        public int? Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Link to page")]
        [UIHint("tinymce_jquery_full"), AllowHtml]
        public string Content { get; set; }
    }

    public class QueryModel
    {
        [Display(Name ="Name")]
        public string Name { get; set; }

        [Display(Name ="Email")]
        public string Email { get; set; }

        [Display(Name ="Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name ="Query")]
        public string Query { get; set; }
    }

    public class BlogModel
    {
        public int? Id { get; set; }

        [Required]
        [Display(Name ="Name")]
        public string BlogName { get; set; }

        [Required]
        [Display(Name ="Content")]
        [UIHint("tinymce_jquery_full"), AllowHtml]
        public string BlogContent { get; set; }

        [Required]
        [Display(Name ="Author")]
        public string BloggerName { get; set; }

        public string ImageName { get; set; }

        [Display(Name = "Blog Image")]
        public HttpPostedFileBase File { get; set; }

        public string BlogDate { get; set; }
    }

    public class HelpAndAdviceCategoryModel
    {
        public int? Id { get; set; }
        [Required]
        public string Category { get; set; }
    }

    public class HelpAndAdviceDetailModel
    {
        public int? Id { get; set; }
        [Required]
        [Display(Name ="Heading")]
        public string Heading { get; set; }
        [Required]
        [Display(Name = "Sub - Heading")]
        public string SubHeading { get; set; }
        [Required]
        [Display(Name ="Category")]
        public int CategoryId { get; set; }
        [Required]
        [UIHint("tinymce_jquery_full"), AllowHtml]
        [Display(Name = "Content")]
        public string Content { get; set; }

        public DateTime LastUpdated { get; set; }
    }

    public class CategoryByArticle
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int ArticleNumber { get; set; }
    }
}