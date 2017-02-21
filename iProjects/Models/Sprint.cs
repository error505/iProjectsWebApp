using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iProjects.Models
{
    public class Sprint
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Sprint Name")]
        public string Name { get; set; }

        [AllowHtml]
        [Display(Name = "Sprint Duration")]
        public string Duration { get; set; }

        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        [Display(Name = "End Time")]
        public DateTime EndTime { get; set; }

         [Display(Name = "Sprint For Project")]
        public int ProjectId { get; set; }

        public virtual Project Project { get; set; }

        private DateTime _projCreatedDate = DateTime.Now;

        [DataType(DataType.Date)]
        public DateTime TimeOfCreation { get { return _projCreatedDate; } set { _projCreatedDate = value; } }

        [Display(Name = "Creator")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Display(Name = "Start Sprint")]
        public bool IsActive { get; set; }

        [Display(Name = "Sprint Finished")]
        public bool Finished { get; set; }
    }
}