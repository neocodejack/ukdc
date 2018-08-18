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
    }

    public class MenuDropDown
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}