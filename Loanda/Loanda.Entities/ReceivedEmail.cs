using Loanda.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Loanda.Entities
{
    public class ReceivedEmail : BaseEntity
    {
        [Required]
        public string SenderEmail { get; set; }

        public string SenderName { get; set; }

        public DateTime DateReceived { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public ICollection<EmailAttachment> EmailAttachments { get; set; }

        public EmailStatus EmailStatus { get; set; }

        public int EmailStatusId { get; set; }

        public Applicant Applicant { get; set; }

        public Guid ApplicantId { get; set; }

        public bool IsReviewed { get; set; } = false; // TODO: Default value to be configured as false
    }
}
