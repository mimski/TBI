using Loanda.Entities;
using Loanda.Services.DTOs;
using Loanda.Services.Mapper.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Loanda.Services.Mapper
{
    public class EmailDtoMapper : IEmailDtoMapper
    {
        public EmailDTO Map(ReceivedEmail recievedEmail)
        {
            return new EmailDTO
            {
                Body = recievedEmail.Body,
                Subject = recievedEmail.Subject,
                DateReceived = recievedEmail.DateReceived,
                SenderEmail = recievedEmail.SenderEmail,
                SenderName = recievedEmail.SenderName
            };
        }

        public ReceivedEmail Map(EmailDTO recievedEmail)
        {
            return new ReceivedEmail
            {
                Body = recievedEmail.Body,
                Subject = recievedEmail.Subject,
                DateReceived = recievedEmail.DateReceived,
                SenderEmail = recievedEmail.SenderEmail,
                SenderName = recievedEmail.SenderName
            };
        }

        //public ICollection<ReceivedEmail> Map(ICollection<EmailDTO> emailDtos)
        //{
        //    return emailDtos.Select(this.Map().ToList());
        //}
    }
}
