using Loanda.Entities;
using Loanda.Services.DTOs;
using Loanda.Services.Mapper.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Loanda.Services.Mapper
{
    //public class EmailDtoMapper : IEmailDtoMapper
    //{
    //    public EmailDTO Map(ReceivedEmailEntity recievedEmail)
    //    {
    //        return new EmailDTO
    //        {
    //            Body = recievedEmail.Body,
    //            Subject = recievedEmail.Subject,

    //            //DateReceived = recievedEmail.DateReceived,
    //            //SenderEmail = recievedEmail.SenderEmail,
    //            //SenderName = recievedEmail.SenderName,
    //            GmailEmailId = recievedEmail.GmailEmailId
    //        };
    //    }

    //    //public ReceivedEmailEntity Map(EmailDTO recievedEmail)
    //    //{
    //    //    return new ReceivedEmailEntity
    //    //    {
    //    //        Body = recievedEmail.Body,
    //    //        Subject = recievedEmail.Subject,
    //    //        From = recievedEmail.From,
    //    //        //DateReceived = recievedEmail.DateReceived,
    //    //        //SenderEmail = recievedEmail.SenderEmail,
    //    //        //SenderName = recievedEmail.SenderName,
    //    //        GmailEmailId = recievedEmail.GmailEmailId,
    //    //    };
    //    //}

    //    //public IReadOnlyCollection<ReceivedEmailEntity> Map(IReadOnlyCollection<EmailDTO> emailDtos)
    //    //{
    //    //    return emailDtos.Select(this.Map).ToList();
    //    //}

    //    //public IReadOnlyCollection<EmailDTO> Map(IReadOnlyCollection<ReceivedEmailEntity> emailDtos)
    //    //{
    //    //    return emailDtos.Select(this.Map).ToList();
    //    //}
    //}
}
