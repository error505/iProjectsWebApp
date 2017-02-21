using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iProjects.Models
{
    public class Dictionary
    {
        public int Id { get; set; }

        public string ForeignWord { get; set; }        

        public string Pronunciation { get; set; }

        public string Translate { get; set; }

        [AllowHtml]
        public string Example { get; set; }

        public string GenderKind { get; set; }

        [AllowHtml]
        public string Additional { get; set; }
    }
}