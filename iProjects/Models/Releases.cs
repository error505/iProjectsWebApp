using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace iProjects.Models
{
    public class Releases
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Name Of Release")]
        public string Name { get; set; }

        [Display(Name = "Version Of Release")]
        public string Version { get; set; }

        [Display(Name = "Name Of the Project")]
        public int ProjectId { get; set; }

        public virtual Project Project { get; set; }

        private DateTime _projCreatedDate = DateTime.Now;

        [DataType(DataType.Date)]
        public DateTime TimeOfCreation { get { return _projCreatedDate; } set { _projCreatedDate = value; } }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        [AllowHtml]
        public string Description { get; set; }

        [Display(Name = "Status")]
        public int StatusId { get; set; }

        public virtual StatusOfStoriesTasks StatusOfStoriesTasks { get; set; }
       
    }
}