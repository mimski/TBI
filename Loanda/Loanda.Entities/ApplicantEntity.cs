using Loanda.Entities.Base;
using System;
using System.Collections.Generic;

namespace Loanda.Entities
{
    public class ApplicantEntity : BaseEntity
    {
        public string EGN { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string EmailAddress { get; set; }

        public virtual ICollection<LoanApplicationEntity> LoanApplication { get; set; }
    }
}
