using System;

namespace Loanda.Entities
{
    public class EmailAttachmentEntity
    {
        public int Id { get; set; }

        public double FileSizeInMb { get; set; }

        public string AttachmentName { get; set; }

        public virtual ReceivedEmailEntity ReceivedEmail { get; set; }

        public long ReceivedEmailId { get; set; }
    }
}
