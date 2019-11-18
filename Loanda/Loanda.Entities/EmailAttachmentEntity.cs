using System;

namespace Loanda.Entities
{
    public class EmailAttachmentEntity
    {
        public int Id { get; set; }

        public double FileSizeInMb { get; set; }

        public string Content { get; set; }

        public virtual ReceivedEmailEntity ReceivedEmail { get; set; }

        public Guid ReceivedEmailId { get; set; }
    }
}
