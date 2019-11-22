using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Loanda.Web.Models.Email
{
    public class EmailViewModel
    {
        public long Id { get; set; }

        [Display(Name = "Sender Email")]
        public string SenderEmail { get; set; }

        [Display(Name = "Sender Name")]
        public string SenderName { get; set; }

        [Display(Name = "Received On")]
        [DataType(DataType.Date)]
        public string DateReceived { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        //public ICollection<EmailAttachment> EmailAttachments { get; set; }

        //public EmailStatus EmailStatus { get; set; }

        public int? EmailStatusId { get; set; }

        //public virtual Applicant Applicant { get; set; }

        public Guid? ApplicantId { get; set; }

        public string GmailEmailId { get; set; }

        [Display(Name = "Created On")]
        [DataType(DataType.Date)]
        public DateTime? CreatedOn { get; set; }

        [Display(Name = "Modified On")]
        [DataType(DataType.Date)]
        public DateTime? ModifiedOn { get; set; }

        [Display(Name = "Deleted On")]
        [DataType(DataType.Date)]
        public DateTime? DeletedOn { get; set; }

        [Display(Name = "Deleted")]
        public bool IsDeleted { get; set; }

        [Display(Name = "Att.")]
        public int TotalAttachments { get; set; }

        [Display(Name = "Att. (Mb)")]
        public double AttachmentsTotalSizeInMB { get; set; }
    }
}
