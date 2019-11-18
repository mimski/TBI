using Loanda.Entities.Base;
using System;
using System.Collections.Generic;

namespace Loanda.Entities
{
    public class ReceivedEmailEntity : BaseEntity
    {
        public string SenderEmail { get; set; }

        public string SenderName { get; set; }

        public DateTime DateReceived { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public virtual ICollection<EmailAttachmentEntity> EmailAttachments { get; set; }

        public virtual EmailStatusEntity EmailStatus { get; set; }

        public int? EmailStatusId { get; set; }

        public virtual ApplicantEntity Applicant { get; set; }

        public Guid? ApplicantId { get; set; }

        public bool IsReviewed { get; set; }
        
        public string GmailEmailId { get; set; }

        public int TotalAttachments { get; set; }

        public double AttachmentsTotalSizeInMB { get; set; }

        public string ProcessedById { get; set; }

        public virtual User ProcessedBy { get; set; }
    }
}
