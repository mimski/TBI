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

        public async Task<bool> AddAsync(LoanApplication loanApplication, CancellationToken cancellationToken)
        {
            var application = loanApplication.ToEntity();
            application.ApplicationStatusId = -1;
            //application.OpenedById = loanApplication.OpenedById;

            var addedLoanApplicationEntry = this.context.LoanApplications.Add(application);

            await this.context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<IReadOnlyCollection<LoanApplication>> GetAllAsync(string userId, CancellationToken cancellationToken)
        {
            var loanApplications = await this.context.LoanApplications.Where(x=> x.OpenedById.Equals(userId) && x.ApplicationStatusId == -1).Include(x => x.OpenedBy).OrderByDescending(application => application.LoanAmount).AsNoTracking().ToListAsync(cancellationToken);
            
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

        public async Task<bool> RemoveAsync(long id, CancellationToken cancellationToken)
        {
            var loanApplication = await this.context.LoanApplications.SingleOrDefaultAsync(application => application.EmailId.Equals(id), cancellationToken);
            if (loanApplication != null)
            {
                this.context.Remove(loanApplication);
                await this.context.SaveChangesAsync(cancellationToken);
                return true;
            }
            else
            {
                return false;
            }
        }



        public async Task<bool> RejectAsync(LoanApplication loanApplication, CancellationToken cancellationToken)
        {
            var existingApplication = await this.context.LoanApplications.SingleOrDefaultAsync(a => a.Id.Equals(loanApplication.Id), cancellationToken);
            if (existingApplication != null)
            {
                existingApplication.ApplicationStatusId = -3;

                this.context.LoanApplications.Update(existingApplication);
                await this.context.SaveChangesAsync(cancellationToken);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> ApproveAsync(LoanApplication loanApplication, CancellationToken cancellationToken)
        {
            var existingApplication = await this.context.LoanApplications.SingleOrDefaultAsync(a => a.Id.Equals(loanApplication.Id), cancellationToken);
            if (existingApplication != null)
            {
                existingApplication.ApplicationStatusId = -2;

                this.context.LoanApplications.Update(existingApplication);
                await this.context.SaveChangesAsync(cancellationToken);
                return true;
            }
            else
            {
                return false;
            }
        }

        //public async Task<bool> ChangeStatusToDefaultAsync(long id, CancellationToken cancellationToken)
        //{
        //    var existingApplication = await this.context.LoanApplications.SingleOrDefaultAsync(a => a.Id.Equals(loanApplication.Id), cancellationToken);
        //    if (existingApplication != null)
        //    {
        //        existingApplication.ApplicationStatusId = -1;

        //        this.context.LoanApplications.Update(existingApplication);
        //        await this.context.SaveChangesAsync(cancellationToken);
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
    }
}
