using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iProjects.Models
{
    
    public class Project
    {

        [Key]
        public int ProjectId { get; set; }

        private DateTime _projCreatedDate = DateTime.Now;

        [DataType(DataType.Date)]
        public DateTime TimeOfCreation { get { return _projCreatedDate; } set { _projCreatedDate = value; } }

        [Required]
        [Display(Name = "Project Name")]
        public string ProjectTitle { get; set; }

        [Required]
        [AllowHtml]
        public string Description { get; set; }

        [DataType(DataType.Url)]
        [Display(Name = "Link for Resource")]
        public string Url { get; set; }

        [Display(Name = "Project Category")]
        public int? ProjectCategoryId { get; set; }

        [Display(Name = "Project Category")]
        public virtual ProjectCategory ProjectCategory { get; set; }

        [Display(Name = "Client")]
        public int ClientId { get; set; }

        public virtual Clients Client { get; set; }

        [Display(Name = "Project Budget")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Budget { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Total Time Spent")]
        public string TotalTimeSpent { get; set; }

        [Display(Name = "Assign User To Project")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Display(Name = "Priority")]
        public int? PriorityId { get; set; }

        public virtual Priority Priority { get; set; }

        public bool Finished { get; set; }

        public int? StatusId { get; set; }

        public virtual StatusOfStoriesTasks StatusOfStoriesTasks { get; set; }

        public virtual ICollection<ApplicationUser> UserOnProject { get; set; }

        public virtual ICollection<TasksToDo> Tasks { get; set; }

        public virtual ICollection<ProjectCategory> ProjectCategorysCategories { get; set; }

        public virtual ICollection<ProjectFilesDb> ProjectFIles { get; set; }

    }

}