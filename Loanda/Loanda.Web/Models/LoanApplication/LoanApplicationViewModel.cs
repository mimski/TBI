using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Loanda.Web.Models.LoanApplication
{
    public class LoanApplicationViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Loan Amount")]
        [Required]
        public decimal LoanAmount { get; set; }

        public Guid ApplicantId { get; set; }

        public int ApplicationStatusId { get; set; }

        [Display(Name = "Approved")]
        public bool? IsApproved { get; set; }

        [Display(Name = "Opened By Id")]
        public string OpenedById { get; set; }

        [Display(Name = "Opened By")]
        public string OpenedBy { get; set; }

        [Display(Name = "Closed By Id")]
        public string ClosedById { get; set; }

        [Display(Name = "Closed By")]
        public string ClosedBy { get; set; }
    }
}
