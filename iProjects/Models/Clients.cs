using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iProjects.Models
{
    public class Clients
    {
        [Key]
        public Int32 ClientId { get; set; }

        public string Name { get; set; }
       
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Company { get; set; }
        [DataType(DataType.Html)]
        public string Website { get; set; }

        public string Skype { get; set; }

        [DataType(DataType.Html)]
        public string Facebook { get; set; }
        [DataType(DataType.Html)]
        public string LinkedIn { get; set; }
        [DataType(DataType.Html)]
        public string Twitter { get; set; }

        public string Note { get; set; }

    }
}