﻿using System;
using System.Collections.Generic;
using Loanda.Entities;

namespace Loanda.Services.Models
{
    public class ReceivedEmail
    {
        public long Id { get; set; }

        public string SenderEmail { get; set; }

        public string SenderName { get; set; }

        public string DateReceived { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public virtual ICollection<EmailAttachmentEntity> EmailAttachments { get; set; }

        public virtual EmailStatusEntity EmailStatus { get; set; }

        public int? EmailStatusId { get; set; }

        public virtual ApplicantEntity Applicant { get; set; }

        public Guid? ApplicantId { get; set; }

        public string GmailEmailId { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public int TotalAttachments { get; set; }

        public double AttachmentsTotalSizeInMB { get; set; }
    }
}
