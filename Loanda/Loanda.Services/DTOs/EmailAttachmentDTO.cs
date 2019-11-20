
namespace Loanda.Services.DTOs
{
    public class EmailAttachmentDTO
    {
        public double FileSizeInMb { get; set; }

        public string AttachmentName { get; set; }

        public long ReceivedEmailId { get; set; }
    }
}
