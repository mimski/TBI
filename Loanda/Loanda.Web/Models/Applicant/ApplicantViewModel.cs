using System;
using System.ComponentModel.DataAnnotations;

namespace Loanda.Web.Models.Applicant
{
    public class ApplicantViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string EGN { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Phone Number")]
        [Required]
        //[Range(10, 10)]
        [MaxLength(10)]
        [MinLength(10)]
        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        [Display(Name = "E-mail")]
        public string EmailAddress { get; set; }

        [Display(Name = "Created On")]
        [DataType(DataType.Date)]
        public DateTime? CreatedOn { get; set; }

        [Display(Name = "Modified On")]
        [DataType(DataType.Date)]
        public DateTime? ModifiedOn { get; set; }

        [Display(Name = "Deleted On")]
        [DataType(DataType.Date)]
        public DateTime? DeleteOn { get; set; }

        [Display(Name = "Deleted")]
        [DataType(DataType.Date)]
        public bool IsDeleted { get; set; }

       public long EmailId { get; set; }
    }
}
