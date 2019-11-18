using Loanda.Services.Models;
using Loanda.Web.Models.Applicant;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Loanda.Web.Mappings
{
    internal static class ApplicantMappings
    {
        public static ApplicantViewModel ToViewModel(this Applicant entity)
        {
            return entity != null ? new ApplicantViewModel
            {
                Id = entity.Id,
                EGN = entity.EGN,
                FirstName = entity.FirstName,
                MiddleName = entity.MiddleName,
                LastName = entity.LastName,
                DateOfBirth = entity.DateOfBirth,
                PhoneNumber = entity.PhoneNumber,
                EmailAddress = entity.EmailAddress,
                Address = entity.Address,
                City = entity.City
            } : null;
        }

        public static Applicant ToServiceModel(this ApplicantViewModel viewModel)
        {
            return viewModel != null ? new Applicant
            {
                Id = viewModel.Id,
                EGN = viewModel.EGN,
                FirstName = viewModel.FirstName,
                MiddleName = viewModel.MiddleName,
                LastName = viewModel.LastName,
                DateOfBirth = viewModel.DateOfBirth,
                PhoneNumber = viewModel.PhoneNumber,
                EmailAddress = viewModel.EmailAddress,
                Address = viewModel.Address,
                City = viewModel.City
            } : null;
        }

        public static IReadOnlyCollection<ApplicantViewModel> ToViewModel(this IReadOnlyCollection<Applicant> entities)
        {
            if (entities.Count == 0)
            {
                return Array.Empty<ApplicantViewModel>();
            }

            var loanApplications = new ApplicantViewModel[entities.Count];

            var index = 0;

            foreach (var entity in entities)
            {
                loanApplications[index] = entity.ToViewModel();
                ++index;
            }

            return new ReadOnlyCollection<ApplicantViewModel>(loanApplications);
        }
    }
}
