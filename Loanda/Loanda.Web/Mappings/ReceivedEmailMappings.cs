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
                IsReviewed = entity.IsReviewed
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
                IsReviewed = viewModel.IsReviewed
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
