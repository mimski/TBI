using Loanda.Entities;
using Loanda.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loanda.Services.Mappings
{
    internal static class ApplicantMappings
    {
        public static Applicant ToService(this ApplicantEntity entity)
        {
            return entity != null
                ? new Applicant
                {
                    Id = entity.Id,
                    EGN = entity.EGN,
                    DateOfBirth = entity.DateOfBirth,
                    FirstName = entity.FirstName,
                    MiddleName = entity.MiddleName,
                    LastName = entity.LastName,
                    PhoneNumber = entity.PhoneNumber,
                    EmailAddress = entity.EmailAddress,
                    Address = entity.Address,
                    City = entity.City
                }
                : null;
        }

        public static ApplicantEntity ToEntity(this Applicant model)
        {
            return model != null
                ? new ApplicantEntity
                {
                    Id = model.Id,
                    EGN = model.EGN,
                    DateOfBirth = model.DateOfBirth,
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    EmailAddress = model.EmailAddress,
                    Address = model.Address,
                    City = model.City,
                    CreatedOn = model.CreatedOn,
                    ModifiedOn = model.ModifiedOn,
                    DeletedOn = model.DeleteOn,
                    IsDeleted = model.IsDeleted
                }
                : null;
        }

        public static IReadOnlyCollection<Applicant> ToService(this IReadOnlyCollection<ApplicantEntity> entities)
            => entities.MapCollection(ToService);
    }
}
