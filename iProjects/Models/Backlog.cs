using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iProjects.Models
{
    public class Backlog
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Backlog")]
        public string Name { get; set; }
        
    }
}