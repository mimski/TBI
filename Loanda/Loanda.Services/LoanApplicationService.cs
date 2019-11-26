using Loanda.Data.Context;
using Loanda.Services.Contracts;
using Loanda.Services.Mappings;
using Loanda.Services.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Loanda.Services
{
    public class LoanApplicationService : ILoanApplicationService
    {
        private readonly LoandaContext context;

        public LoanApplicationService(LoandaContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<LoanApplication> AddAsync(LoanApplication loanApplication, CancellationToken cancellationToken)
        {
            var application = loanApplication.ToEntity();
            application.ApplicationStatusId = -1;

            var addedLoanApplicationEntry = this.context.LoanApplications.Add(application);

            await this.context.SaveChangesAsync(cancellationToken);
            return application.ToService();
        }

        public async Task<IReadOnlyCollection<LoanApplication>> GetAllAsync(CancellationToken cancellationToken)
        {
            var loanApplications = await this.context.LoanApplications.AsNoTracking().OrderByDescending(application => application.LoanAmount).ToListAsync(cancellationToken);
            return loanApplications.ToService();
        }

        public async Task<LoanApplication> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var loanApplication = await this.context.LoanApplications.AsNoTracking().SingleOrDefaultAsync(application => application.Id.Equals(id), cancellationToken);
            return loanApplication.ToService();
        }

        //public async Task<LoanApplication> UpdateAsync(LoanApplication loanApplication, CancellationToken ct)
        //{
        //    var updatedLoanApplicationEntry = this.context.LoanApplications.Update(loanApplication.ToEntity());
        //    await this.context.SaveChangesAsync(ct);
        //    return updatedLoanApplicationEntry.Entity.ToService();
        //}

        public async Task<LoanApplication> UpdateAsync(LoanApplication loanApplication, CancellationToken cancellationToken)
        {
            var existingLoanApplication = await this.context.LoanApplications.SingleOrDefaultAsync(application => application.Id.Equals(loanApplication.Id), cancellationToken);
            this.context.Entry(existingLoanApplication).CurrentValues.SetValues(loanApplication.ToEntity());
            await this.context.SaveChangesAsync();

            return existingLoanApplication.ToService();
        }

        public async Task<LoanApplication> MarkAsDeletedAsync(Guid id, CancellationToken cancellationToken)
        {
            var loanApplication = await this.context.LoanApplications.SingleOrDefaultAsync(application => application.Id.Equals(id), cancellationToken);
            if (loanApplication != null)
            {
                loanApplication.IsDeleted = true;
                loanApplication.DeletedOn = DateTime.UtcNow.AddHours(2);

                this.context.LoanApplications.Update(loanApplication);
                await this.context.SaveChangesAsync(cancellationToken);
                return loanApplication.ToService();
            }
            else
            {
                return null;
            }
        }

        public async Task RemoveAsync(Guid id, CancellationToken cancellationToken)
        {
            var loanApplication = await this.context.LoanApplications.SingleOrDefaultAsync(application => application.Id.Equals(id), cancellationToken);
            if (loanApplication != null)
            {
                this.context.Remove(loanApplication);
                await this.context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
