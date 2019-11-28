using Loanda.Entities;
using Loanda.Services.DTOs;
using Loanda.Services.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Loanda.Services.Contracts
{
    public interface IEmailService
    {
        Task<long> CreateAsync(EmailDTO emailDto);

        Task<IReadOnlyCollection<ReceivedEmail>> GetAllAsync(CancellationToken cancellationToken);

        Task<IReadOnlyCollection<ReceivedEmail>> GetAllNotReviewedAsync(CancellationToken cancellationToken);

        Task<IReadOnlyCollection<ReceivedEmail>> GetAllNewAsync(CancellationToken cancellationToken);

        Task<IReadOnlyCollection<ReceivedEmail>> GetAllInvalidAsync(CancellationToken cancellationToken);

        Task<ReceivedEmail> FindByIdAsync(long id, CancellationToken cancellationToken);

        Task<bool> ChangeEmailStatusToNewAsync(long id, CancellationToken cancellationToken);

        Task<bool> MarkInvalidAsync(ReceivedEmail receivedEmail, CancellationToken cancellationToken);

        Task<bool> MarkNotReviewedAsync(ReceivedEmail receivedEmail, CancellationToken cancellationToken);

        Task<bool> ChangeToOpenAsync(long emailId, CancellationToken cancellationToken);

        Task<IReadOnlyCollection<ReceivedEmail>> GetAllOpenAsync(string userId, CancellationToken cancellationToken);

        Task<IReadOnlyCollection<ReceivedEmail>> GetAllClosedAsync(CancellationToken cancellationToken);

        Task<bool> ChangeToCloseAsync(Guid loanId, CancellationToken cancellationToken);

        Task<bool> UpdateAsync(EmailDTO emailDto);
    }
}
