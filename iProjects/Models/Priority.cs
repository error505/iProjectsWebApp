using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iProjects.Models
{
    public class Priority
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Priority")]
        public string Name { get; set; }
    }
}