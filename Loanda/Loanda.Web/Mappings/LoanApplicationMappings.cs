using Loanda.Services.Models;
using Loanda.Web.Models.LoanApplication;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Loanda.Web.Mappings
{
    internal static class LoanApplicationMappings
    {
        public static LoanApplicationViewModel ToViewModel(this LoanApplication entity)
        {
            return entity != null ? new LoanApplicationViewModel
            {
                Id = entity.Id,
                LoanAmount = entity.LoanAmount,
                ApplicantId = entity.ApplicantId,
                ApplicationStatusId = entity.ApplicationStatusId,
                ClosedBy = entity.ClosedBy,
                OpenedBy = entity.OpenedBy,
                ClosedById = entity.ClosedById,
                OpenedById = entity.OpenedById,
                EmailId = entity.EmailId
            } : null;
        }

        public static LoanApplication ToServiceModel(this LoanApplicationViewModel viewModel)
        {
            return viewModel != null ? new LoanApplication
            { 
                Id = viewModel.Id,
                LoanAmount = viewModel.LoanAmount ,
                ApplicantId = viewModel.ApplicantId,
                ApplicationStatusId = viewModel.ApplicationStatusId,
                ClosedBy = viewModel.ClosedBy,
                OpenedBy = viewModel.OpenedBy,
                ClosedById = viewModel.OpenedById,
                OpenedById = viewModel.ClosedById,
                EmailId = viewModel.EmailId
            } : null;
        }

        public static IReadOnlyCollection<LoanApplicationViewModel> ToViewModel(this IReadOnlyCollection<LoanApplication> entities)
        {
            if (entities.Count == 0)
            {
                return Array.Empty<LoanApplicationViewModel>();
            }

            var loanApplications = new LoanApplicationViewModel[entities.Count];
            
            var index = 0;

            foreach (var entity in entities)
            {
                loanApplications[index] = entity.ToViewModel();
                ++index;
            }

            return new ReadOnlyCollection<LoanApplicationViewModel>(loanApplications);
        }
    }
}
