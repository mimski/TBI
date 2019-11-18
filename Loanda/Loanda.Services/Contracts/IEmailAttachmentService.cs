﻿using Loanda.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Loanda.Services.Contracts
{
    public interface IEmailAttachmentService
    {
        Task<EmailAttachmentEntity> AddAttachmentAsync(Guid emailId, string content, long size);
    }
}
