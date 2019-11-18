using Loanda.Entities.Base;
using System;

namespace Loanda.Entities
{
    public class LoanApplicationEntity : BaseEntity
    {
        public virtual ApplicantEntity Applicant { get; set; }

        public Guid ApplicantId { get; set; }

        public virtual ApplicationStatusEntity ApplicationStatus { get; set; }

        public int ApplicationStatusId { get; set; }

        public decimal LoanAmount { get; set; }

        public bool? IsApproved { get; set; }
    }
}
