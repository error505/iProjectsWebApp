using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iProjects.Models
{
    public class Invoice
    {
        [Key]
        public int PaymentId { get; set; }

        public string Type { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Amount { get; set; }

        public string Title { get; set; }

        [AllowHtml]
        public string Description { get; set; }

        [Display(Name = "Payment Methode")]
        public string PaymentMethode { get; set; }

        public string InvoiceNumber { get; set; }

        [Display(Name = "Payment For Project")]
        public int ProjectId { get; set; }

        public virtual Project Project { get; set; }

        public string Client { get; set; }

        public string Company { get; set; }
        
        public string Country { get; set; }

        public string Town { get; set; }

        public string Street { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Display(Name = "Account Number")]
        [DataType(DataType.CreditCard)]
        public string AccountNr { get; set; }

        public string Swift { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public decimal Tax { get; set; }

        public decimal Discount { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Total { get; set; }

        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; }

        [Display(Name = "Payment is Done")]
        public bool IsPayed { get; set; }
    }
}