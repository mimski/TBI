using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Loanda.Services.Contracts
{
    public interface IApplicantService
    {
        Task<Guid> CreateAsync(string firstName, string middleName, string lastName, string egn, string phoneNumber, string adress, string city);

        Task<bool> EditAsync(Guid id, string firstName, string middleName, string lastName, string egn, string phoneNumber, string adress, string city);

        Task<bool> ExistsAsync(Guid id);
    }
}
