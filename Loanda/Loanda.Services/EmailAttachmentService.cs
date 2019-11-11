using Loanda.Data.Context;
using Loanda.Entities;
using Loanda.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Loanda.Services
{
    public class EmailAttachmentService : IEmailAttachmentService
    {
        private readonly LoandaContext context;

        public EmailAttachmentService(LoandaContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<EmailAttachment> AddAttachmentAsync(Guid emailId, string content, long size)
        {
            var attachment = new EmailAttachment
            {
                Content = content,
                FileSizeInMb = size,
                ReceivedEmailId = emailId
            };

            this.context.EmailAttachments.Add(attachment);

            await this.context.SaveChangesAsync();

            return attachment;
        }
    }
}
