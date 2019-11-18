using Loanda.Entities;
using Loanda.Services.DTOs;
using Loanda.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Loanda.Services.Mappings
{
    public static class ReceivedEmailMappings
    {
        public static ReceivedEmail ToService(this ReceivedEmailEntity entity)
        {
            return entity != null
                ? new ReceivedEmail
                {
                    Id = entity.Id,
                    Body = entity.Body,
                    Subject = entity.Subject,
                    SenderEmail = entity.SenderEmail,
                    SenderName = entity.SenderName,
                    GmailEmailId = entity.GmailEmailId,
                    DateReceived = entity.DateReceived,
                    ApplicantId = entity.ApplicantId,
                    EmailStatusId = entity.EmailStatusId,
                    IsReviewed = entity.IsReviewed
                }
                : null;
        }

        public static ReceivedEmailEntity ToEntity(this EmailDTO dto)
        {
            return dto != null
                ? new ReceivedEmailEntity
                {
                    Body = dto.Body,
                    Subject = dto.Subject,
                    //SenderEmail = "emailaat",
                    //SenderName = "sendernamaat",
                    //DateReceived = recievedEmail.DateReceived,
                    SenderEmail = dto.From.Split(new[] { '<', '>' }, StringSplitOptions.RemoveEmptyEntries).ToList().Last().Trim(),
                    SenderName = dto.From.Split(new[] { '<', '>' }, StringSplitOptions.RemoveEmptyEntries).ToList().First().Trim(),
                    GmailEmailId = dto.GmailEmailId,
                }
                : null;
        }

        public static IReadOnlyCollection<ReceivedEmail> ToService(this IReadOnlyCollection<ReceivedEmailEntity> entities)
            => entities.MapCollection(ToService);
    }
}
