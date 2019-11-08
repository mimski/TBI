using Loanda.Data.Context;
using Loanda.Entities;
using Loanda.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Loanda.Services
{
    public class EmailService : IEmailService
    {
        private readonly LoandaContext context;

        public EmailService(LoandaContext context)
        {
            this.context = context;
        }

        public async Task<Guid> Create(string senderEmail, DateTime DateReceived, string subject, string body)
        {
            var email = new ReceivedEmail
            {
                SenderEmail = senderEmail,
                DateReceived = DateReceived,
                Subject = subject,
                Body = body
            };

            this.context.Add(email);

            await this.context.SaveChangesAsync();

            return email.Id;
        }

        //public async Task<IEnumerable<EmailService>> GetAll()
        //{
        //    return await this.context.ReceivedEmails.ToListAsync();  
        //}

    }
}
