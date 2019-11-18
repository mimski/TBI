using System;

namespace Loanda.Web.Models.Applicant
{
    public class ApplicantViewModel
    {
        public Guid Id { get; set; }

        public string EGN { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string EmailAddress { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public DateTime? DeleteOn { get; set; }

        public bool IsDeleted { get; set; }
    }
}
