using Loanda.Entities;
using Loanda.Services.Models;
using System.Collections.Generic;

namespace Loanda.Services.Mappings
{
    internal static class LoanApplicationMappings
    {
        public static LoanApplication ToService(this LoanApplicationEntity entity)
        {
            return entity != null
                ? new LoanApplication 
                { 
                    Id = entity.Id,
                    LoanAmount = entity.LoanAmount,
                    ApplicantId = entity.ApplicantId,
                    ApplicationStatusId = entity.ApplicationStatusId,
                    ClosedBy = entity.ClosedBy.UserName,
                    OpenedBy = entity.OpenedBy.UserName,
                    ClosedById = entity.ClosedById,
                    OpenedById = entity.OpenedById,
                    EmailId = entity.EmailId
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
                    EmailId = model.EmailId
                }
                : null;
        }

        public static IReadOnlyCollection<LoanApplication> ToService(this IReadOnlyCollection<LoanApplicationEntity> entities)
            => entities.MapCollection(ToService);
    }
}
