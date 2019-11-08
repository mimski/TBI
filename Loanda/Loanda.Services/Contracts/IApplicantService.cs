using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Loanda.Services.Contracts
{
    public interface IApplicantService
    {
        Task<Guid> Create(string firstName, string middleName, string lastName, string egn, string phoneNumber, string adress, string city);

        Task<bool> Edit(Guid id, string firstName, string middleName, string lastName, string egn, string phoneNumber, string adress, string city);

        Task<bool> Exists(int id);
    }
}
