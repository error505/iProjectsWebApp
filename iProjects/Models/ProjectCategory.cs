using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace iProjects.Models
{
    public class ProjectCategory
    {
        [Key]
        public Int32 ProjectCategoryId { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Project> Projects { get; set; }

    }
}