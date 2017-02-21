using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace iProjects.Models
{
    /// <summary>
    /// Model Class For Tasks
    /// </summary>
    /// Igor Iric
    public class TasksToDo
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Task Title")]
        public string Title { get; set; }

        [AllowHtml]
        public string Description { get; set; }

        [Display(Name = "Complette Status")]
        public bool IsDone { get; set; }

        [Display(Name = "Assing User")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<ApplicationUser> UserTasks { get; set; }

        [Display(Name = "Project")]
        public int ProjectId { get; set; }

        public virtual Project Project { get; set; }

        [Display(Name = "Releated To story")]
        public int StoriesId { get; set; }

        public virtual Story Story { get; set; }

        private DateTime _projCreatedDate = DateTime.Now;

        [DataType(DataType.Date)]
        public DateTime TimeOfCreation { get { return _projCreatedDate; } set { _projCreatedDate = value; } }

        [Display(Name = "Status")]
        public int StatusId { get; set; }

        public virtual StatusOfStoriesTasks StatusOfStoriesTasks { get; set; }

        [Display(Name = "Fix Version")]
        public int ReleaseId { get; set; }

        public virtual Releases Releases { get; set; }

        [Display(Name = "Priority")]
        public string PrioritiesId { get; set; }

        public virtual Priority Priority { get; set; }

        [Display(Name = "Sprint")]
        public int SprintsId { get; set; }

        public virtual Sprint Sprint { get; set; }

        [Display(Name = "Backlog")]
        public bool Backlog { get; set; }

        public string Revision { get; set; }

        [Display(Name = "Integration Test Reference")]
        public string TestReference { get; set; }

        [Display(Name = "Unit Test Reference")]
        public string UnitTestReference { get; set; }
    }
}