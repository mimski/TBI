using Loanda.Entities.Base;
using System;

namespace Loanda.Entities
{
    public class LoanApplication : BaseEntity
    {
        public virtual Applicant Applicant { get; set; }

        public Guid ApplicantId { get; set; }

        public virtual ApplicationStatus ApplicationStatus { get; set; }

        public int ApplicationStatusId { get; set; }
    }
}
