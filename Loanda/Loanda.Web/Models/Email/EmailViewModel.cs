using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loanda.Web.Models.Email
{
    public class EmailViewModel
    {
        public Guid Id { get; set; }

        public string SenderEmail { get; set; }

        public string SenderName { get; set; }

        public DateTime DateReceived { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        //public ICollection<EmailAttachment> EmailAttachments { get; set; }

        //public EmailStatus EmailStatus { get; set; }

        //public int? EmailStatusId { get; set; }

        //public virtual Applicant Applicant { get; set; }

        //public Guid? ApplicantId { get; set; }

        public bool IsReviewed { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
