using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iProjects.Models
{
    public class Epic
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Epic Name")]
        public string Name { get; set; }

        [AllowHtml]
        [Display(Name = "Epic Descritpion")]
        public string Description { get; set; }
    }
}