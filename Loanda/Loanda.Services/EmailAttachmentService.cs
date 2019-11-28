using Loanda.Data.Context;
using Loanda.Services.Contracts;
using System;
using System.Threading.Tasks;
using Loanda.Services.Mappings;
using Loanda.Services.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Loanda.Services
{
    public class EmailAttachmentService : IEmailAttachmentService
    {
        private readonly LoandaContext context;

        public EmailAttachmentService(LoandaContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> AddAttachmentAsync(EmailAttachmentDTO emailAttachmentDto)
        {
            this.context.EmailAttachments.Add(emailAttachmentDto.ToEntity());
            await this.context.SaveChangesAsync();

            return true;
        }
    }
}
