using Loanda.Entities;
using Loanda.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loanda.Services.Mapper.Contracts
{
    public interface IEmailAttachmentDtoMapper
    {
        EmailAttachmentDTO Map(EmailAttachment emailAttachment);
        EmailAttachment Map(EmailAttachmentDTO emailAttachmentDto);
    }
}
