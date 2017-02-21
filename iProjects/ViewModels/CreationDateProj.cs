using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iProjects.ViewModels
{
    public class CreationDateProj
    {
        [DataType(DataType.Date)]
        public DateTime? CreationDate { get; set; }

        public int ProjectCount { get; set; }
    }
}