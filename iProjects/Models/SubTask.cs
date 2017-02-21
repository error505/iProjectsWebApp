using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iProjects.Models
{
    public class SubTask
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
        [AllowHtml]
        public string Description { get; set; }

        [Display(Name = "Complette Status")]
        public bool IsDone { get; set; }

        [Display(Name = "Assing User")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<ApplicationUser> UserTasks { get; set; }

        [Display(Name = "Releted Task")]
        public int TaskId { get; set; }

        public virtual TasksToDo TasksToDo { get; set; }

        [Display(Name = "Status")]
        public int StatusId { get; set; }

        public virtual StatusOfStoriesTasks StatusOfStoriesTasks { get; set; }

        private DateTime _projCreatedDate = DateTime.Now;

        [DataType(DataType.Date)]
        public DateTime TimeOfCreation { get { return _projCreatedDate; } set { _projCreatedDate = value; } }

        [Display(Name = "Fix Version")]
        public int ReleasesId { get; set; }

        public virtual Releases Releases { get; set; }

        [Display(Name = "Sprint")]
        public int SprintId { get; set; }

        public virtual Sprint Sprint { get; set; }

        [Display(Name = "Priority")]
        public string PriorityId { get; set; }

        public virtual Priority Priority { get; set; }

        [Display(Name = "Backlog")]
        public bool Backlog { get; set; }

    }
}