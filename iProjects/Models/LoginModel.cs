using System.ComponentModel.DataAnnotations;

namespace iProjects.Models
{
    public class LoginModel
    {

        [Required]
        public string Name { get; set; }
    }
}