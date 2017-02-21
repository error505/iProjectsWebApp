using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iProjects.Models
{
    public class CrashReport
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Subject/Title:")]
        public string Title { get; set; }

        [AllowHtml]
        [Display(Name = "Crash Description")]       
        public string Description { get; set; }

        private DateTime _projCreatedDate = DateTime.Now;

        [Display(Name = "Time Of Creatiion")]
        [DataType(DataType.Date)]
        public DateTime Created { get { return _projCreatedDate; } set { _projCreatedDate = value; } }

        [Display(Name = "Created By:")]
        public string Creator { get; set; }

        [Display(Name = "Name Of The Product")]
        public string ProductName { get; set; }

        [Display(Name = "OS/Platform")]
        public string Os { get; set; }

        [Required]
        [Display(Name = "Crash Details")]
        public string CrsdhDetails { get; set; }

        public bool Status { get; set; }


    }
}