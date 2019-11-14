using Loanda.Entities;
using Loanda.Services.DTOs;
using Loanda.Services.Mapper.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Loanda.Services.Mapper
{
    public class EmailAttachmentDtoMapper : IEmailAttachmentDtoMapper
    {
        public EmailAttachmentDTO Map(EmailAttachment emailAttachment)
        {
            return new EmailAttachmentDTO
            {
                Content = emailAttachment.Content,
                FileSizeInMb = emailAttachment.FileSizeInMb
            };
        }

        public EmailAttachment Map(EmailAttachmentDTO emailAttachmentDto)
        {
            return new EmailAttachment
            {
                Content = emailAttachmentDto.Content,
                FileSizeInMb = emailAttachmentDto.FileSizeInMb
            };
        }

        public ICollection<EmailAttachment> Map(ICollection<EmailAttachmentDTO> emailAttachmentDto)
        {
            return emailAttachmentDto.Select(this.Map).ToList();
        }

        public ICollection<EmailAttachmentDTO> Map(ICollection<EmailAttachment> emailAttachmentDto)
        {
            return emailAttachmentDto.Select(this.Map).ToList();
        }
    }
}
