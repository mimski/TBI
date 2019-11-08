using Loanda.Data.Context;
using Loanda.Entities;
using Loanda.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Loanda.Services
{
    public class ApplicantService : IApplicantService
    {
        private readonly LoandaContext context;

        public ApplicantService(LoandaContext context)
        {
            this.context = context;
        }

        public async Task<Guid> Create(string firstName, string middleName, string lastName, string egn, string phoneNumber, string adress, string city)
        {
            var applicant = new Applicant
            {
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                EGN = egn,
                PhoneNumber = phoneNumber,
                Address = adress,
                City = city
            };

            this.context.Add(applicant);

            await this.context.SaveChangesAsync();

            return applicant.Id;
        }

        public async Task<bool> Edit(Guid id, string firstName, string middleName, string lastName, string egn, string phoneNumber, string adress, string city)
        {
            var applicant = await this.context.Applicants.FindAsync(id);

            if (applicant == null)
            {
                return false;
            }

            applicant.FirstName = firstName;
            applicant.MiddleName = middleName;
            applicant.LastName = lastName;
            applicant.EGN = egn;
            applicant.PhoneNumber = phoneNumber;
            applicant.Address = adress;
            applicant.City = city;

            this.context.Applicants.Update(applicant);

            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Exists(int id)
        {
            return await this.context.Applicants.AnyAsync(applicant => applicant.Equals(id));
        }
    }
}
