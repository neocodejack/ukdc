using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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
}