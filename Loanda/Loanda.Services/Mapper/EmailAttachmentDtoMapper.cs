using Loanda.Entities;
using Loanda.Services.DTOs;
using Loanda.Services.Mapper.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
