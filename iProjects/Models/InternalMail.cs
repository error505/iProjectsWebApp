using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iProjects.Models
{
    public class MailFile
    {
        [Required]
        [DisplayName("Select File to Upload")]
        public IEnumerable<HttpPostedFileBase> AttFile { get; set; }
        //2nd change
    }
    public class InternalMail
    {
        [Key]
        public int Id { get; set; }

        private DateTime _projCreatedDate = DateTime.Now;

        [DataType(DataType.Date)]
        public DateTime TimeOfCreation { get { return _projCreatedDate; } set { _projCreatedDate = value; } }

        [Display(Name = "Subject:")]
        public string Subject { get; set; }

        [AllowHtml]
        public string Message { get; set; }

        [Display(Name = "From:")]
        public string From { get; set; }

        [Display(Name = "To:")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string FileName { get; set; }

        public byte[] AttachmentFile { get; set; }

        public bool IsRead { get; set; }

        public bool Archive { get; set; }

        public bool Spam { get; set; }
    }
}