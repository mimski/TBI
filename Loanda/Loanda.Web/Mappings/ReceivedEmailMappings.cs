using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Loanda.Services.Models;
using Loanda.Web.Models.Email;

namespace Loanda.Web.Mappings
{
    internal static class ReceivedEmailMappings
    {
        public static EmailViewModel ToViewModel(this ReceivedEmail entity)
        {
            return entity != null ? new EmailViewModel
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
            } : null;
        }

        public static ReceivedEmail ToServiceModel(this EmailViewModel viewModel)
        {
            return viewModel != null ? new ReceivedEmail
            {
                Id = viewModel.Id,
                Body = viewModel.Body,
                Subject = viewModel.Subject,
                SenderEmail = viewModel.SenderEmail,
                SenderName = viewModel.SenderName,
                GmailEmailId = viewModel.GmailEmailId,
                DateReceived = viewModel.DateReceived,
                ApplicantId = viewModel.ApplicantId,
                EmailStatusId = viewModel.EmailStatusId,
                CreatedOn = viewModel.CreatedOn,
                ModifiedOn = viewModel.ModifiedOn,
                DeletedOn = viewModel.DeletedOn,
                IsDeleted = viewModel.IsDeleted,
            } : null;
        }

        public static IReadOnlyCollection<EmailViewModel> ToViewModel(this IReadOnlyCollection<ReceivedEmail> entities)
        {
            if (entities.Count == 0)
            {
                return Array.Empty<EmailViewModel>();
            }

            var loanApplications = new EmailViewModel[entities.Count];

            var index = 0;

            foreach (var entity in entities)
            {
                loanApplications[index] = entity.ToViewModel();
                ++index;
            }

            return new ReadOnlyCollection<EmailViewModel>(loanApplications);
        }
    }
}
