using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loanda.Web.Models.LoanApplication
{
    public class LoanApplicationViewModel
    {
        public Guid Id { get; set; }

        public decimal LoanAmount { get; set; }

        public Guid ApplicantId { get; set; }

        public int ApplicationStatusId { get; set; }

        public bool? IsApproved { get; set; }

        public string OpenedById { get; set; }

        public string OpenedBy { get; set; }

        public string ClosedById { get; set; }

        public string ClosedBy { get; set; }
    }
}
