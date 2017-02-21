using System;
using System.ComponentModel.DataAnnotations;

namespace iProjects.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        public string Type { get; set; }

        public string Amount { get; set; }

        public string Title { get; set; }

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

        public string Phone { get; set; }

        [Display(Name = "Account Number")]
        public string AccountNr { get; set; }

        public string Swift { get; set; }

        public string Email { get; set; }

        public int Tax { get; set; }

        public int Discount { get; set; }

        public int Total { get; set; }        

        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; }

        [Display(Name = "Payment is Done")]
        public bool IsPayed { get; set; }
    }
}