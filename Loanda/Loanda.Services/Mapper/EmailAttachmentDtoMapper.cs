using Loanda.Entities;
using Loanda.Services.DTOs;
using Loanda.Services.Mapper.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Loanda.Services.Mapper
{
    //public class EmailAttachmentDtoMapper : IEmailAttachmentDtoMapper
    //{
    //    public EmailAttachmentDTO Map(EmailAttachmentEntity emailAttachment)
    //    {
    //        return new EmailAttachmentDTO
    //        {
    //            Content = emailAttachment.Content,
    //            FileSizeInMb = emailAttachment.FileSizeInMb
    //        };
    //    }

    //    public EmailAttachmentEntity Map(EmailAttachmentDTO emailAttachmentDto)
    //    {
    //        return new EmailAttachmentEntity
    //        {
    //            Content = emailAttachmentDto.Content,
    //            FileSizeInMb = emailAttachmentDto.FileSizeInMb
    //        };
    //    }

    //    public IReadOnlyCollection<EmailAttachmentEntity> Map(IReadOnlyCollection<EmailAttachmentDTO> emailAttachmentDto)
    //    {
    //        return emailAttachmentDto.Select(this.Map).ToList();
    //    }

    //    public IReadOnlyCollection<EmailAttachmentDTO> Map(IReadOnlyCollection<EmailAttachmentEntity> emailAttachmentDto)
    //    {
    //        return emailAttachmentDto.Select(this.Map).ToList();
    //    }
    //}
}
