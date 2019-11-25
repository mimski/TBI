using System;
using System.Collections.Generic;
using System.Text;

namespace Loanda.Services.Models
{
    public class LoanApplication
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

        public long EmailId { get; set; }
    }
}
