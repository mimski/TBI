using Loanda.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Loanda.Entities
{
    public class Applicant : BaseEntity
    {
        [Required]
        public string EGN { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        public ICollection<LoanApplication> LoanApplication { get; set; }
    }
}
