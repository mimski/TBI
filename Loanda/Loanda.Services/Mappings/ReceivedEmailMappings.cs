using Loanda.Entities;
using Loanda.Services.DTOs;
using Loanda.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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
                    CreatedOn = entity.CreatedOn,
                    ModifiedOn = entity.ModifiedOn,
                    DeletedOn = entity.DeletedOn,
                    IsDeleted = entity.IsDeleted,
                    TotalAttachments = entity.TotalAttachments,
                    AttachmentsTotalSizeInMB = Math.Round(entity.AttachmentsTotalSizeInMB,2)
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
                    DateReceived = dto.DateReceived,
                    SenderEmail = dto.From.Split(new[] { '<', '>' }, StringSplitOptions.RemoveEmptyEntries).ToList().Last().Trim(),
                    SenderName = dto.From.Split(new[] { '<', '>' }, StringSplitOptions.RemoveEmptyEntries).ToList().First().Trim(),
                    GmailEmailId = dto.GmailEmailId,
                    TotalAttachments = dto.TotalAttachments,
                    AttachmentsTotalSizeInMB = dto.AttachmentsTotalSizeInMB,
                }
                : null;
        }

        public static IReadOnlyCollection<ReceivedEmail> ToService(this IReadOnlyCollection<ReceivedEmailEntity> entities)
            => entities.MapCollection(ToService);
    }
}
