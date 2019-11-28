using Loanda.Data.Context;
using Loanda.Services.Contracts;
using Loanda.Services.DTOs;
using Loanda.Services.Mappings;
using Loanda.Services.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            email.Subject = emailDto.Subject;
            email.Body = emailDto.Body;
            email.AttachmentsTotalSizeInMB = (double)emailDto.AttachmentsTotalSizeInMB;
            email.TotalAttachments = emailDto.TotalAttachments;

            this.context.ReceivedEmails.Update(email);
            await this.context.SaveChangesAsync();
            return email.ToService();
        }

        public async Task<IReadOnlyCollection<ReceivedEmail>> GetAllInvalidAsync(CancellationToken cancellationToken)
        {
            var emails = await this.context.ReceivedEmails.Where(e => e.EmailStatusId.Equals(-3)).AsNoTracking().ToListAsync(cancellationToken);

            return emails.ToService();
        }

        public async Task<IReadOnlyCollection<ReceivedEmail>> GetAllAsync(CancellationToken cancellationToken)
        {
            var emails = await this.context.ReceivedEmails.Where(e => e.EmailStatusId.Equals(-1)).AsNoTracking().ToListAsync(cancellationToken);

            foreach (var email in emails)
            {
                email.Body = Base64Decode(email.Body);
            }

            return emails.ToService();
        }

        public async Task<IReadOnlyCollection<ReceivedEmail>> GetAllNotReviewedAsync(CancellationToken cancellationToken)
        {
            var emails = await this.context.ReceivedEmails.Where(e => e.EmailStatusId.Equals(-1)).AsNoTracking().ToListAsync(cancellationToken);

            foreach (var email in emails)
            {
                email.Body = Base64Decode(email.Body ?? string.Empty);
            }

            return emails.ToService();
        }

        public async Task<IReadOnlyCollection<ReceivedEmail>> GetAllNewAsync(CancellationToken cancellationToken)
        {
            var emails = await this.context.ReceivedEmails.Where(e => e.EmailStatusId.Equals(-2)).AsNoTracking().ToListAsync(cancellationToken);

            foreach (var email in emails)
            {
                email.Body = Base64Decode(email.Body);
            }

            return emails.ToService();
        }

        public async Task<IReadOnlyCollection<ReceivedEmail>> GetAllOpenAsync(string userId, CancellationToken cancellationToken)
        {
            var emails = await this.context.ReceivedEmails.Where(e => e.EmailStatusId.Equals(-4))
                .Include(x => x.LoanApplication)
                .Where(la => la.LoanApplication.OpenedById == userId && la.LoanApplication.ApplicationStatusId != -4)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            foreach (var email in emails)
            {
                email.Body = Base64Decode(email.Body);
            }

            return emails.ToService();
        }

        public async Task<ReceivedEmail> FindByIdAsync(long id, CancellationToken cancellationToken)
        {
            var email = await this.context.ReceivedEmails.AsNoTracking().SingleOrDefaultAsync(e => e.Id.Equals(id), cancellationToken);

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

        public async Task<bool> MarkInvalidAsync(ReceivedEmail receivedEmail, CancellationToken cancellationToken)
        {
            var existingEmail = await this.context.ReceivedEmails.SingleOrDefaultAsync(email => email.Id.Equals(receivedEmail.Id), cancellationToken);
            if (existingEmail != null)
            {
                existingEmail.EmailStatusId = -3;
                existingEmail.DeletedOn = DateTime.UtcNow.AddHours(2);

                this.context.ReceivedEmails.Update(existingEmail);
                await this.context.SaveChangesAsync(cancellationToken);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> MarkNotReviewedAsync(ReceivedEmail receivedEmail, CancellationToken cancellationToken)
        {
            var existingEmail = await this.context.ReceivedEmails.SingleOrDefaultAsync(email => email.Id.Equals(receivedEmail.Id), cancellationToken);
            if (existingEmail != null)
            {
                existingEmail.EmailStatusId = -1;

                this.context.ReceivedEmails.Update(existingEmail);
                await this.context.SaveChangesAsync(cancellationToken);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> ChangeEmailStatusToNewAsync(long id, CancellationToken cancellationToken)
        {
            var existingEmail = await this.context.ReceivedEmails.SingleOrDefaultAsync(email => email.Id.Equals(id), cancellationToken);
            if (existingEmail != null)
            {
                existingEmail.EmailStatusId = -2;
                existingEmail.ModifiedOn = DateTime.UtcNow.AddHours(2);

                this.context.ReceivedEmails.Update(existingEmail);
                await this.context.SaveChangesAsync(cancellationToken);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> ChangeToOpenAsync(long emailId, CancellationToken cancellationToken)
        {
            var existingEmail = await this.context.ReceivedEmails.SingleOrDefaultAsync(email => email.Id.Equals(emailId), cancellationToken);
            if (existingEmail != null)
            {
                existingEmail.EmailStatusId = -4;

                this.context.ReceivedEmails.Update(existingEmail);
                await this.context.SaveChangesAsync(cancellationToken);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> ChangeToCloseAsync(Guid loanId, CancellationToken cancellationToken)
        {
            var existingEmail = await this.context.ReceivedEmails.SingleOrDefaultAsync(email => email.LoanApplication.Id.Equals(loanId), cancellationToken);
            if (existingEmail != null)
            {
                existingEmail.EmailStatusId = -5;

                this.context.ReceivedEmails.Update(existingEmail);
                await this.context.SaveChangesAsync(cancellationToken);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(EmailDTO emailDto)
        {
            var existingApplicant = await this.context.ReceivedEmails.SingleOrDefaultAsync(a => a.Id.Equals(emailDto.Id));
            var email = emailDto.ToEntity();
            email.EmailStatusId = -1;

            this.context.Entry(existingApplicant).CurrentValues.SetValues(email);
            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<IReadOnlyCollection<ReceivedEmail>> GetAllClosedAsync(CancellationToken cancellationToken)
        {
            var emails = await this.context.ReceivedEmails.Where(e => e.EmailStatusId.Equals(-5))
               .Include(x => x.LoanApplication)
               .AsNoTracking()
               .ToListAsync(cancellationToken);

            foreach (var email in emails)
            {
                email.Body = Base64Decode(email.Body);
            }

            return emails.ToService();
        }
    }
}
