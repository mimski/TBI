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
    }
}
