using Loanda.Entities;
using Loanda.Services.Models;
using System.Collections.Generic;

namespace Loanda.Services.Mappings
{
    public static class LoanApplicationMappings
    {
       
            public static LoanApplication ToService(this LoanApplicationEntity entity)
            {
            return entity != null
                ? new LoanApplication
                {
                    Id = entity.Id,
                    LoanAmount = entity.LoanAmount,
                    ApplicantId = entity.ApplicantId,
                    OpenedBy = entity.OpenedBy?.UserName,
                    ClosedBy = entity.ClosedBy?.UserName,
                    EmailId = entity.EmailId,
                    IsApproved = entity.IsApproved,
                    ClosedById = entity.ClosedById,
                    OpenedById = entity.OpenedById,
                    ApplicationStatusId = entity.ApplicationStatusId
                    }
                    : null;
            }

            public static LoanApplicationEntity ToEntity(this LoanApplication model)
            {
                return model != null
                    ? new LoanApplicationEntity
                    {
                        Id = model.Id,
                        LoanAmount = model.LoanAmount,
                        ApplicantId = model.ApplicantId,
                       ApplicationStatusId = model.ApplicationStatusId,
                       OpenedById = model.OpenedById,
                       ClosedById = model.ClosedById,
                       IsApproved = model.IsApproved,
                       EmailId = model.EmailId,
                    }
                    : null;
            }

            public static IReadOnlyCollection<LoanApplication> ToService(this IReadOnlyCollection<LoanApplicationEntity> entities)
                => entities.MapCollection(ToService);
        }
    }
