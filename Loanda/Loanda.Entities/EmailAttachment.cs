using System;
using System.Collections.Generic;
using System.Text;

namespace Loanda.Entities
{
    public class EmailAttachment
    {
        public int Id { get; set; }
        public long FileSizeInMb { get; set; }
        public string Content { get; set; }
        public ReceivedEmail ReceivedEmail { get; set; }
        public Guid ReceivedEmailId { get; set; }
    }
}
