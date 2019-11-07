using Loanda.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loanda.Entities
{
    public class LoanApplication : BaseEntity
    {
        public Applicant Applicant { get; set; }

        public Guid ApplicantId { get; set; }

        public ApplicationStatus ApplicationStatus { get; set; }

        public int ApplicationStatusId { get; set; }
    }
}
