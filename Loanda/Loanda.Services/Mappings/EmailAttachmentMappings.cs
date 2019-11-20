using Loanda.Entities;
using Loanda.Services.DTOs;

namespace Loanda.Services.Mappings
{
    internal static class EmailAttachmentMappings
    {
        public static EmailAttachmentEntity ToEntity(this EmailAttachmentDTO dto)
        {
            return dto != null
                ? new EmailAttachmentEntity
                {
                    ReceivedEmailId = dto.ReceivedEmailId,
                    AttachmentName = dto.AttachmentName,
                    FileSizeInMb = dto.FileSizeInMb,
                }
                : null;
        }
    }
}
