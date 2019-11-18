using Loanda.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Loanda.Services.Contracts
{
    public interface IApplicantService
    {
        //Task<Guid> CreateAsync(string firstName, string middleName, string lastName, string egn, string phoneNumber, string adress, string city);

        //Task<bool> EditAsync(Guid id, string firstName, string middleName, string lastName, string egn, string phoneNumber, string adress, string city);

        //Task<bool> ExistsAsync(Guid id);

        Task<Applicant> AddAsync(Applicant applicant, CancellationToken ct);

        Task<IReadOnlyCollection<Applicant>> GetAllAsync(CancellationToken ct);

        Task<Applicant> GetByIdAsync(Guid id, CancellationToken ct);

        Task<Applicant> UpdateAsync(Applicant applicant, CancellationToken ct);

        Task<Applicant> MarkAsDeletedAsync(Guid id, CancellationToken ct);

        Task RemoveAsync(Guid id, CancellationToken ct);
    }
}
