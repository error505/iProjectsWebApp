using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace iProjects.Models
{
    public class StatusOfStoriesTasks
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Status { get; set; }

    }
}