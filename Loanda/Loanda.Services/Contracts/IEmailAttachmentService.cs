using Loanda.Entities;
using Loanda.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Loanda.Services.Contracts
{
    public interface IEmailAttachmentService
    {
        Task<bool> AddAttachmentAsync(EmailAttachmentDTO emailAttachmentDto);
    }
}
