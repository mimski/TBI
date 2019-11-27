using Loanda.Entities.Base.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Loanda.Entities
{
    public class User : IdentityUser, IAuditable, IDeletable
    {
        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
        public bool IsFirstLogin { get; set; }

        public virtual ICollection<LoanApplicationEntity> OpenLoanApplications { get; set; }

        public virtual ICollection<LoanApplicationEntity> ClosedLoanApplication { get; set; }

        public virtual ICollection<ReceivedEmailEntity> ProcessedEmails { get; set; }
    }
}
