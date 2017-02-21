using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iProjects.Models
{
    public class BugReport
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Subject/Title:")]
        public string Title { get; set; }

        [Display(Name = "Resolved In Revision")]
        public string Revision { get; set; }

        [Display(Name = "Bug Type")]
        public int BugTypeId { get; set; }

        public virtual BugType BugType { get; set; }

        [Display(Name = "Bug Description")]
        public string Description { get; set; }

        private DateTime _bugCreatedDate = DateTime.Now;

        [Display(Name = "Time Of Creation")]
        public DateTime Created { get { return _bugCreatedDate; } set { _bugCreatedDate = value; } }

        [Display(Name = "Created By:")]
        public string Creator { get; set; }

        [Display(Name = "Name Of The Product")]
        public string ProductName { get; set; }

        [Display(Name = "Project")]
        public int ProjectsId { get; set; }

        public virtual Project Project { get; set; }

        [Required]
        [AllowHtml]
        [Display(Name = "Bug Details")]
        public string BugDetails { get; set; }

        public bool Status { get; set; }

        public int StatusId { get; set; }

        public virtual StatusOfStoriesTasks StatusOfStoriesTasks { get; set; }

        public int ReleaseId { get; set; }

        public virtual Releases Releases { get; set; }

        [Display(Name = "Assign To  User")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Display(Name = "Sprint")]
        public int SprintsId { get; set; }

        public virtual Sprint Sprint { get; set; }

        [Display(Name = "Backlog")]
        public bool Backlog { get; set; }

        [Display(Name = "Integration Test Reference")]
        public string TestReference { get; set; }

        [Display(Name = "Unit Test Reference")]
        public string UnitTestReference { get; set; }
    }

}