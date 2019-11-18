using System;
using System.Collections.Generic;
using System.Text;

namespace Loanda.Services.DTOs
{
    public class EmailDTO
    {
        public string SenderEmail { get; set; }

        public string SenderName { get; set; }

        public string From { get; set; }

        //public DateTime DateReceived { get; set; }
        public string DateReceived { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string GmailEmailId { get; set; }

        public int TotalAttachments { get; set; }

        public double? AttachmentsTotalSizeInMB { get; set; }

        public virtual ICollection<EmailAttachmentDTO> EmailAttachments { get; set; }
    }
}
