using Loanda.Services.DTOs;
using Loanda.Web.Mappers.Contracts;
using Loanda.Web.Models.Email;

namespace Loanda.Web.Mappers
{
    public class EmailViewModelMapper : IMapper<EmailDTO, EmailViewModel>
    {
        public EmailViewModel Map(EmailDTO entity)
        {
            var emailViewModel = new EmailViewModel
            {
                //Id = entity.Id,
                Subject = entity.Subject,
                Body = entity.Body,
                DateReceived = entity.DateReceived,
                //CreatedOn = entity.CreatedOn,
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
