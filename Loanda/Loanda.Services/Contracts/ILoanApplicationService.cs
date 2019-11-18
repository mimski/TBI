using Loanda.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Loanda.Services.Contracts
{
    public interface ILoanApplicationService
    {
        Task<IReadOnlyCollection<LoanApplication>> GetAllAsync(CancellationToken ct);

        Task<LoanApplication> GetByIdAsync(Guid id, CancellationToken ct);

        Task<LoanApplication> UpdateAsync(LoanApplication loanApplication, CancellationToken ct);

        Task<LoanApplication> AddAsync(LoanApplication loanApplication, CancellationToken ct);

        Task<LoanApplication> MarkAsDeletedAsync(Guid id, CancellationToken ct);

        Task RemoveAsync(Guid id, CancellationToken ct);
    }
}
