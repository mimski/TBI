﻿using Loanda.Entities;
using Loanda.Web.Mappers.Contracts;
using Loanda.Web.Models.Email;

namespace Loanda.Web.Mappers
{
    public class EmailViewModelMapper : IMapper<ReceivedEmailEntity, EmailViewModel>
    {
        public EmailViewModel Map(ReceivedEmailEntity entity)
        {
            var emailViewModel = new EmailViewModel
            {
                Id = entity.Id,
                Subject = entity.Subject,
                Body = entity.Body,
                DateReceived = entity.DateReceived,
                SenderEmail = entity.SenderEmail,
                SenderName = entity.SenderName,
                CreatedOn = entity.CreatedOn,
                ModifiedOn = entity.ModifiedOn,
                IsDeleted = entity.IsDeleted,

                //EmailStatusType = entity.EmailStatus.Type,
                //EmailAttachmentsCount = entity.EmailAttachments.Count,
                //Attachments = entity.EmailAttachments,
                //UserName = entity.User?.UserName,
                //TimeInCurrentStatus = DateTime.Now.Subtract(entity.ModifiedOn).ToString(@"dd\.hh\:mm\:ss")
            };

            return emailViewModel;
        }
    }
}
