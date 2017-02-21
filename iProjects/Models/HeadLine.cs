using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iProjects.Models
{
    public class HeadLineOne
    {        
            public int Id { get; set; }

            [AllowHtml]
            public string Message { get; set; }               
    }
}