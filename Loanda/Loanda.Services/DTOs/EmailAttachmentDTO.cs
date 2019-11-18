using System;
using System.Collections.Generic;
using System.Text;

namespace Loanda.Services.DTOs
{
    public class EmailAttachmentDTO
    {
        public double? FileSizeInMb { get; set; }

        public string Content { get; set; }
    }
}
