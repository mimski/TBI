using Loanda.Data.Context;
using Loanda.Entities;
using Loanda.Services.Contracts;
using Loanda.Services.DTOs;
using Loanda.Services.Mapper;
using Loanda.Services.Mapper.Contracts;
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
        //private readonly IEmailDtoMapper dtoMapper;

        public EmailService(LoandaContext context/*, IEmailDtoMapper dtoMapper*/)
        {
            this.context = context;
            //this.dtoMapper = dtoMapper;
        }

        //public async Task<bool> CreateAsync(EmailDTO emailDto)
        //{
        //    var email = dtoMapper.Map(emailDto);

        //    this.context.Add(email);

        //    await this.context.SaveChangesAsync();

        //    return true;
        //}

        public async Task<bool> CreateAsync(EmailDTO emailDto)
        {
            this.context.ReceivedEmails.Add(emailDto.ToEntity());
            await this.context.SaveChangesAsync();
            return true;
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
            email.IsReviewed = true;
          
            this.context.ReceivedEmails.Update(email);
            await this.context.SaveChangesAsync();
            return email.ToService();
        }


        public async Task<IReadOnlyCollection<ReceivedEmailEntity>> GetAllAsync()
        {
            var emails = await this.context.ReceivedEmails.ToListAsync();

            return emails;
        }

        public async Task<ReceivedEmail> FindByIdAsync(Guid id, CancellationToken ct)
        {
            var email = await this.context.ReceivedEmails.AsNoTracking().SingleOrDefaultAsync(e => e.Id.Equals(id), ct);
            return email.ToService();

            //var email = await this.context.ReceivedEmails.FirstOrDefaultAsync(e => e.Id.Equals(id));

            //if (email == null)
            //{
            //    throw new ArgumentException("There is no such email");
            //}

            //return email;
        }

        public static string Base64Decode(string base64EncodedData)
        {
            base64EncodedData = base64EncodedData.Replace('-', '+');
            base64EncodedData = base64EncodedData.Replace('_', '/');
            byte[] encode = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(encode);
        }

    }
}
