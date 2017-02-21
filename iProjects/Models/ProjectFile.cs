using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace iProjects.Models
{
    
   
    public class ProjectFile
    {
        [Required]
        [DisplayName("Select File to Upload")]
        public IEnumerable<HttpPostedFileBase> File { get; set; }  //2nd change

    }
    public class ProjectFilesDb
    {
        [Key]
        public int Id { get; set; }
        public string FileName { get; set; }
        public byte[] File { get; set; }

       
    }

}