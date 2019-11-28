using Loanda.Data.Context;
using Loanda.Services.Contracts;
using Loanda.Services.Mappings;
using Loanda.Services.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Loanda.Services
{
    public class ApplicantService : IApplicantService
    {
        private readonly LoandaContext context;

        public ApplicantService(LoandaContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Applicant> AddAsync(Applicant applicant, CancellationToken cancellationToken)
        {
            var addedApplicantEntry = this.context.Applicants.Add(applicant.ToEntity());
            await this.context.SaveChangesAsync(cancellationToken);
            return addedApplicantEntry.Entity.ToService();
        }

        public async Task<IReadOnlyCollection<Applicant>> GetAllAsync(CancellationToken cancellationToken)
        {
            var applicants = await this.context.Applicants.AsNoTracking().ToListAsync(cancellationToken);
            return applicants.ToService();
        }

        public async Task<Applicant> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var applicant = await this.context.Applicants.AsNoTracking().SingleOrDefaultAsync(a => a.Id.Equals(id), cancellationToken);
            return applicant.ToService();
        }

        public async Task<Applicant> GetByEgnAsync(string id, CancellationToken cancellationToken)
        {
            var applicant = await this.context.Applicants.AsNoTracking().SingleOrDefaultAsync(a => a.EGN.Equals(id), cancellationToken);
            return applicant.ToService();
        }

        public async Task<Applicant> UpdateAsync(Applicant applicant, CancellationToken cancellationToken)
        {
            var existingApplicant = await this.context.Applicants.SingleOrDefaultAsync(a => a.Id.Equals(applicant.Id), cancellationToken);
            this.context.Entry(existingApplicant).CurrentValues.SetValues(applicant.ToEntity());
            await this.context.SaveChangesAsync();

            return existingApplicant.ToService();
        }

        public async Task<int> CountAsync()
        {
            return await context.Applicants.CountAsync();
        }

        public async Task<Applicant> MarkAsDeletedAsync(Guid id, CancellationToken cancellationToken)
        {
            var applicant = await this.context.Applicants.SingleOrDefaultAsync(a => a.Id.Equals(id), cancellationToken);
            if (applicant != null)
            {
                applicant.IsDeleted = true;
                applicant.DeletedOn = DateTime.UtcNow.AddHours(2);

                this.context.Applicants.Update(applicant);
                await this.context.SaveChangesAsync(cancellationToken);
                return applicant.ToService();
            }
            else
            {
                return null;
            }
        }

        public async Task RemoveAsync(Guid id, CancellationToken cancellationToken)
        {
            var applicant = await this.context.Applicants.SingleOrDefaultAsync(a => a.Id.Equals(id), cancellationToken);
            if (applicant != null)
            {
                this.context.Remove(applicant);
                await this.context.SaveChangesAsync(cancellationToken);
            }
        }

        //public async Task<Guid> CreateAsync(string firstName, string middleName, string lastName, string egn, string phoneNumber, string adress, string city)
        //{
        //    var applicant = new ApplicantEntity
        //    {
        //        FirstName = firstName,
        //        MiddleName = middleName,
        //        LastName = lastName,
        //        EGN = egn,
        //        PhoneNumber = phoneNumber,
        //        Address = adress,
        //        City = city
        //    };

        //    this.context.Add(applicant);

        //    await this.context.SaveChangesAsync();

        //    return applicant.Id;
        //}

        //public async Task<bool> EditAsync(Guid id, string firstName, string middleName, string lastName, string egn, string phoneNumber, string adress, string city)
        //{
        //    var applicant = await this.context.Applicants.FindAsync(id);

        //    if (applicant == null)
        //    {
        //        return false;
        //    }

        //    applicant.FirstName = firstName;
        //    applicant.MiddleName = middleName;
        //    applicant.LastName = lastName;
        //    applicant.EGN = egn;
        //    applicant.PhoneNumber = phoneNumber;
        //    applicant.Address = adress;
        //    applicant.City = city;

        //    this.context.Applicants.Update(applicant);

        //    await this.context.SaveChangesAsync();

        //    return true;
        //}

        //public async Task<bool> ExistsAsync(Guid id)
        //{
        //    return await this.context.Applicants.AnyAsync(applicant => applicant.Equals(id));
        //}
    }
}
