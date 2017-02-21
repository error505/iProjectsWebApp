using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace iProjects.Models
{
    public class Story
    {
        [Key]
        public int StoryId { get; set; }

        [Required]
        [Display(Name = "Story Name")]
        public string Name { get; set; }

        [AllowHtml]
        public string Description { get; set; }

        [AllowHtml]
        [Display(Name = "Acceptance Criteria")]
        public string AcceptanceCriteria { get; set; }

        [Display(Name = "Priority")]
        public int? PriorityId { get; set; }

        public virtual Priority Priority { get; set; }

        [Display(Name = "Status")]
        public int? StatusId { get; set; }

        public virtual StatusOfStoriesTasks StatusOfStoriesTasks { get; set; }

        private DateTime _projCreatedDate = DateTime.Now;

        [DataType(DataType.Date)]
        public DateTime TimeOfCreation { get { return _projCreatedDate; } set { _projCreatedDate = value; } }

        [Display(Name = "Assign To  User")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Display(Name = "Project")]
        public int ProjectsId { get; set; }

        public virtual Project Project { get; set; }

        [Display(Name = "Sprint")]
        public int? SprintsId { get; set; }

        public virtual Sprint Sprint { get; set; }

        [Display(Name = "Epic")]
        public int? EpicId { get; set; }

        public virtual Epic Epic { get; set; }

        [Display(Name = "Fix Version")]
        public int? ReleaseId { get; set; }

        public virtual Releases Releases { get; set; }

        [Display(Name = "Backlog")]
        public bool Backlog { get; set; }

        [Display(Name = "Done")]
        public bool IsDone { get; set; }

        public string Revision { get; set; }

        [Display(Name = "Integration Test Reference")]
        public string TestReference { get; set; }

        [Display(Name = "Unit Test Reference")]
        public string UnitTestReference { get; set; }
    }
}