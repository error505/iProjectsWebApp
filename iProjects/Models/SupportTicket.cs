using System;
using System.ComponentModel.DataAnnotations;

namespace iProjects.Models
{
    public class SupportTicket
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Ticket Title")]
        public string Title { get; set; }

        [Display(Name = "Ticket Description")]
        public string Description { get; set; }

        [Display(Name = "Sender")]
        public string From { get; set; }

        [Display(Name = "Name Of The Project")]
        public string ProjectName { get; set; }

        [Required]
        [Display(Name = "Write a Message")]
        public string Message { get; set; }

        public bool Status { get; set; }

        public int StatusId { get; set; }

        public virtual StatusOfStoriesTasks StatusOfStoriesTasks { get; set; }

    }
}