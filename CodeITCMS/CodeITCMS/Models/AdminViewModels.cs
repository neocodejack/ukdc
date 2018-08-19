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
        public int Id { get; set; }

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
        public int Id { get; set; }

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
        public int Id { get; set; }

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
    }

    public class MenuDropDown
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class PhoneModel
    {
        public int Id { get; set; }
        
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
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Link to page")]
        public string Link { get; set; }

        [Required]
        [Display(Name = "Menu Index")]
        public int TabIndex { get; set; }
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
}