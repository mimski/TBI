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
        Task<IReadOnlyCollection<LoanApplication>> GetAllAsync(string userId, CancellationToken cancellationToken);

        Task<LoanApplication> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<LoanApplication> UpdateAsync(LoanApplication loanApplication, CancellationToken cancellationToken);

        Task<bool> AddAsync(LoanApplication loanApplication, CancellationToken cancellationToken);

        Task<LoanApplication> MarkAsDeletedAsync(Guid id, CancellationToken cancellationToken);

        Task<bool> RemoveAsync(long id, CancellationToken cancellationToken);
        Task<bool> RejectAsync(LoanApplication loanApplication, CancellationToken cancellationToken);

        Task<bool> ApproveAsync(LoanApplication loanApplication, CancellationToken cancellationToken);
    }
}
