using Loanda.Data.Context;
using Loanda.Services.Contracts;
using Loanda.Services.DTOs;
using Loanda.Services.Mappings;
using Loanda.Services.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Loanda.Services
{
    public class EmailService : IEmailService
    {
        private readonly LoandaContext context;

        public EmailService(LoandaContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<long> CreateAsync(EmailDTO emailDto)
        {
            var email = emailDto.ToEntity();
            this.context.ReceivedEmails.Add(email);
            await this.context.SaveChangesAsync();

            return email.Id;
        }

        public async Task<ReceivedEmail> AddEmailData(EmailDTO emailDto)
        {
            var email = await this.context.ReceivedEmails.SingleOrDefaultAsync(a => a.GmailEmailId.Equals(emailDto.GmailEmailId));
            if (email == null)
            {

            }

            email.SenderEmail = emailDto.SenderEmail;
            email.SenderName = emailDto.SenderName;
            email.Subject = emailDto.Subject;
            email.Body = emailDto.Body;
            email.AttachmentsTotalSizeInMB = (double)emailDto.AttachmentsTotalSizeInMB;
            email.TotalAttachments = emailDto.TotalAttachments;
          
            this.context.ReceivedEmails.Update(email);
            await this.context.SaveChangesAsync();
            return email.ToService();
        }

        public async Task<IReadOnlyCollection<ReceivedEmail>> GetAllAsync(CancellationToken cancellationToken)
        {
            var emails = await this.context.ReceivedEmails.AsNoTracking().ToListAsync(cancellationToken);

            return emails.ToService();
        }

        public async Task<ReceivedEmail> FindByIdAsync(long id, CancellationToken cancellationToken)
        {
            var email = await this.context.ReceivedEmails.AsNoTracking().SingleOrDefaultAsync(e => e.Id.Equals(id), cancellationToken);

            Base64Decode(email.Body);

            email.Body = Base64Decode(email.Body);

            return email.ToService();
        }

        private string Base64Decode(string base64EncodedData)
        {
            base64EncodedData = base64EncodedData.Replace('-', '+');
            base64EncodedData = base64EncodedData.Replace('_', '/');
            byte[] encode = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(encode);
        }
    }
}
